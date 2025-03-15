using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.GraphicsBuffer;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class Portal : MonoBehaviour
{
    public Player player;

    [SerializeField] Portal otherPotal;
    Plane plane;
    [SerializeField] bool isRedPortal;
    [SerializeField] Camera portalCamera;
    [SerializeField] float portalWidth;
    [SerializeField] float portalHeight;
    [SerializeField] Image notConnectImg;
    private void Update()
    {
        SetCameraPositon();
        //CameraUpdate();
    }
    void SetCameraPositon()
    {
        Vector3 localPos = otherPotal.transform.InverseTransformPoint(player.cameraContainer.position);
        localPos.z = -localPos.z;
        localPos.x = -localPos.x;
        portalCamera.transform.localPosition = localPos;
        CameraProjectionUpdate();
        Vector3 lookPoint = transform.position + (0.01f *transform.forward);
        portalCamera.transform.localRotation =Quaternion.identity;
        
        //portalCamera.transform.localRotation = FindLookRotate(-portalCamera.transform.localPosition,transform.up);
        //portalCamera.transform.LookAt(lookPoint);
    }
    //Quaternion FindLookRotate(Vector3 targetForward, Vector3 targetUp)
    //{
    //    Vector3 forward = targetForward;
    //    Vector3 right = Vector3.Cross(targetUp, forward);
    //    Vector3 up = Vector3.Cross(forward, right);

    //    return Quaternion.LookRotation(forward, up);
    //}
    


    void CameraProjectionUpdate()
    {
        Vector3 localPos = portalCamera.transform.localPosition;
        float right = -localPos.x + (portalWidth / 2f);
        float left = -localPos.x - (portalWidth / 2f);
        float top = -localPos.y + (portalHeight / 2f);
        float bottom = -localPos.y - (portalHeight / 2f);
        float near = Vector3.Distance(portalCamera.transform.position, transform.position) + 0.01f;
        float far = 300f;

        Matrix4x4 _metrix = new Matrix4x4();
        _metrix[0, 0] = 2f * near / (right - left);
        _metrix[0, 2] = (right + left) / (right - left);
        _metrix[1, 1] = 2f * near / (top - bottom);
        _metrix[1, 2] = (top + bottom) / (top - bottom);
        _metrix[2, 2] = -(far + near) / (far - near);
        _metrix[2, 3] = -(2f * far * near) / (far - near);
        _metrix[3, 2] = -1f;

        portalCamera.projectionMatrix = _metrix;
    }
    //void CameraUpdate()
    //{
    //    float distance = Vector3.Distance(player.transform.position, otherPotal.transform.position);
    //    //portalCamera.fieldOfView = 60f / (1 + distance);
    //    portalCamera.nearClipPlane = distance + 0.3f;
    //}
    public void SetOtherPortal(Portal Potal)
    {
        otherPotal = Potal;
    }
    
    
    public void SummonPortal(Vector3 hitPoint, Vector3 hitNormal)
    {


        this.gameObject.SetActive(true);

        IsConnected();

        Vector3 forward = hitNormal;
        //Vector3 forward = isRedPortal ? hitNormal : -hitNormal;
        Vector3 right = Vector3.Cross(Vector3.up, forward);
        Vector3 up = Vector3.Cross(forward, right);

        transform.rotation = Quaternion.LookRotation(forward, up);
        

        //Vector3 forward = isRedPortal? hitNormal : -hitNormal;
        //Vector3 right = Vector3.Cross(Vector3.up, forward);
        //Vector3 up = Vector3.Cross(forward, right);

        //transform.rotation = Quaternion.LookRotation(forward,up);


        //float rotationY = Vector3.SignedAngle(Vector3.forward * (isRedPortal ? 1f: -1f), hitNormal, Vector3.up);
        //float rotationX = 90f - Vector3.Angle(Vector3.up, hitNormal);

        transform.position = hitPoint;

        plane = new Plane(transform.forward, transform.position);

        bool isPortalOpen = otherPotal.gameObject.activeSelf;
        GetComponent<Collider>().isTrigger = isPortalOpen;
        otherPotal.GetComponent<Collider>().isTrigger = isPortalOpen;
        //transform.rotation = Quaternion.Euler(isRedPortal? rotationX : -rotationX, rotationY, 0);
    }
    void IsConnected()
    {
        bool connected = otherPotal.gameObject.activeSelf;
        notConnectImg.enabled = !connected;
        otherPotal.notConnectImg.enabled = !connected;
    }
    //private void OnTriggerEnter(Collider other)
    //{
    //    other.transform.SetParent(transform);
    //}
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.layer != LayerMask.NameToLayer("Player"))
            return;

        if (plane.GetDistanceToPoint(other.transform.position) >= 0)
            return;
        
        other.transform.SetParent(transform);
        other.transform.SetParent(otherPotal.transform,false);
        Vector3 localPos = other.transform.localPosition;
        other.transform.localPosition = new Vector3(-localPos.x, localPos.y, -localPos.z);


        Vector3 localEuler = other.transform.localRotation.eulerAngles;
        localEuler.y = localEuler.y+180f;
        other.transform.localRotation = Quaternion.Euler(localEuler);


        Rigidbody rb = other.GetComponent<Rigidbody>();
        Vector3 velocity = rb.velocity;
        velocity = transform.InverseTransformDirection(velocity);
        velocity = new Vector3(-velocity.x, velocity.y, -velocity.z);
        velocity = otherPotal.transform.TransformDirection(velocity);
        rb.velocity = velocity;

        other.transform.SetParent(null);

        /*
        //==============================================================
        if (isRedPortal && plane.GetDistanceToPoint(other.transform.position) >= 0)
            return;
        if (!isRedPortal && plane.GetDistanceToPoint(other.transform.position) <= 0)
            return;

        

        Rigidbody rb = other.GetComponent<Rigidbody>();
        Vector3 enterPosition = other.transform.position - transform.position;
        enterPosition = enterPosition - (Vector3.Dot(enterPosition, transform.forward) * (isRedPortal ? transform.forward : -transform.forward));

        Quaternion quaternion = Quaternion.Inverse(transform.rotation) * otherPotal.transform.rotation;
        Debug.Log($"{quaternion}");

        //Quaternion curLocalRotation = other.transform.localRotation;
        //Vector3 curLocalPosition = other.transform.localPosition;
        other.transform.SetParent(transform);
        other.transform.SetParent(otherPotal.transform,false);
        

        //Debug.Log($"preLocalRotation ={other.transform.localRotation}");
        //Quaternion curLocalRotation = other.transform.localRotation;
        //Vector3 curLocalPosition = other.transform.localPosition;
        //Debug.Log($"curLocalRotation ={curLocalRotation}");
        //other.transform.SetParent(otherPotal.transform);
        //Debug.Log($"aftermoveLocalRotation ={other.transform.localRotation}");
        //other.transform.localPosition = curLocalPosition;
        //other.transform.localRotation = curLocalRotation;
        //Debug.Log($"curLocalRotation ={curLocalRotation}");
        //Debug.Log($"afterchangeLocalRotation ={other.transform.localRotation}");

        //other.transform.localPosition = curLocalPosition;
        other.transform.position += (isRedPortal? -otherPotal.transform.forward : otherPotal.transform.forward) * 0.1f;
        //other.transform.localRotation = curLocalRotation;
        other.transform.SetParent(null);
        //other.transform.rotation = Quaternion.Euler(other.transform.rotation.x + otherPotal.transform.rotation.x - transform.rotation.x, other.transform.rotation.y + otherPotal.transform.rotation.y - transform.rotation.y + 180f, other.transform.rotation.z + otherPotal.transform.rotation.z - transform.rotation.z);

        //other.transform.rotation = quaternion * other.transform.rotation;

        rb.velocity = quaternion * rb.velocity;
        //enterPosition = otherPotal.transform.rotation * enterPosition;
        //other.transform.position = otherPotal.transform.position + enterPosition + ((isRedPortal ? -otherPotal.transform.forward : otherPotal.transform.forward) * 0.1f);
        */
    }
    //private void OnTriggerExit(Collider other)
    //{
    //    other.transform.SetParent (null);
    //}

    void Telefort(Collider other)
    {
        if (plane.GetDistanceToPoint(other.transform.position) >= 0)
            return;

        other.transform.SetParent(transform);
        other.transform.SetParent(otherPotal.transform, false);
        Vector3 localPos = other.transform.localPosition;
        other.transform.localPosition = new Vector3(-localPos.x, localPos.y, -localPos.z);


        Vector3 localEuler = other.transform.localRotation.eulerAngles;
        localEuler.y = localEuler.y + 180f;
        other.transform.localRotation = Quaternion.Euler(localEuler);


        Rigidbody rb = other.GetComponent<Rigidbody>();
        Vector3 velocity = rb.velocity;
        velocity = transform.InverseTransformDirection(velocity);
        velocity = new Vector3(-velocity.x, velocity.y, -velocity.z);
        velocity = otherPotal.transform.TransformDirection(velocity);
        rb.velocity = velocity;

        other.transform.SetParent(null);
    }
    public void LaserInput(Vector3 hitPoint,Vector3 startPosition)
    {
        Vector3 laserDirection = hitPoint - startPosition;
        laserDirection = transform.InverseTransformDirection(laserDirection);
        laserDirection = new Vector3(-laserDirection.x,laserDirection.y,-laserDirection.z);

        laserDirection = otherPotal.transform.TransformDirection(laserDirection);

        Vector3 localHitPoint = transform.InverseTransformPoint(hitPoint);

        Vector3 newStartPosition = otherPotal.transform.TransformPoint(localHitPoint) + (otherPotal.transform.forward * 0.01f);
    }


}
