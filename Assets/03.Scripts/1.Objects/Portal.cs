using System.Collections.Generic;
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

    [SerializeField] Camera portalCamera;
    [SerializeField] float portalWidth;
    [SerializeField] float portalHeight;
    [SerializeField] Image notConnectImg;

    [SerializeField] LayerMask canTelefortLayerMask;

    [SerializeField] Animator animator;
    [SerializeField] Collider collider1;
    [SerializeField] Collider collider2;

    [SerializeField] float aditionalPortalExitSpeed;
    private void Update()
    {
        SetCameraPositon();
        //CameraUpdate();
    }
    void SetCameraPositon()             //반대측(반대색깔) 포탈에 비칠 풍경을 찍을 카메라 위치 세팅
    {
        Vector3 localPos = otherPotal.transform.InverseTransformPoint(player.cameraContainer.position);     //반대측 포탈 기준 플레이어의 local좌표 계산
        localPos.z = -localPos.z;                                                                           //계산된 local좌표를 y축으로 180도 회전
        localPos.x = -localPos.x;
        portalCamera.transform.localPosition = localPos;
        CameraProjectionUpdate();                                                                           //중요중요중요, 카메라의 "절두체"를 포탈과의 거리,각도에 맞춰 조정
        Vector3 lookPoint = transform.position + (0.01f * transform.forward);
        portalCamera.transform.localRotation = Quaternion.identity;                                         //카메라 회전은 0으로 고정

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



    void CameraProjectionUpdate()                                       //카메라 절두체 수동 조정(핵심 이지만 글,말로 설명은 못함) portalCamera.projectionMatrix 를 수동으로 조정
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


    public void SummonPortal(Vector3 hitPoint, Vector3 hitNormal)           //포탈 소환 ==>> 포탈의 각도 조정 z축 회전은 0으로 고정
    {


        this.gameObject.SetActive(true);

        IsConnected();


        Vector3 forward = hitNormal;                                        //포탈 forward방향은 소환벽면의 법선과 일치 (수직)
        
        Vector3 right = Vector3.Cross(Vector3.up, forward);                 //포탈의 x축(=right)방향을 월드up방향과 포탈의 forward방향과 수직되게 설정
        Vector3 up = Vector3.Cross(forward, right);                         //포탈의 y축(=up)방향을 계산해둔 포탈 x축(right), z축(up)방향과 수직되게 설정
                                                                                        // == 포탈의 z회전을 0으로 고정 
        transform.rotation = Quaternion.LookRotation(forward, up);


       

        transform.position = hitPoint;                                      //포탈이 생성될 위치

        plane = new Plane(transform.forward, transform.position);           //포탈 통과할때 이동시킬 기준이 될 평면 설정 (해당평면을 지나칠때 이동시킴)

        bool isPortalOpen = otherPotal.gameObject.activeSelf;               //다른 포탈도 활성화 상태일때만 서로 연결된 이미지,기능을 사용가능
        collider1.isTrigger = isPortalOpen;                                 //플레이어와 오브젝트(큐브 등)을 인식할 콜라이더를 따로 설정(범위문제)
        collider2.enabled = isPortalOpen;
        otherPotal.collider1.isTrigger = isPortalOpen;
        otherPotal.collider2.enabled = isPortalOpen;
        
        animator.SetTrigger("PortalOpen");


        //===================================================================== 폐기된 코드들,모아둠

        //Vector3 forward = isRedPortal ? hitNormal : -hitNormal;

        //Vector3 forward = isRedPortal? hitNormal : -hitNormal;
        //Vector3 right = Vector3.Cross(Vector3.up, forward);
        //Vector3 up = Vector3.Cross(forward, right);

        //transform.rotation = Quaternion.LookRotation(forward,up);


        //float rotationY = Vector3.SignedAngle(Vector3.forward * (isRedPortal ? 1f: -1f), hitNormal, Vector3.up);
        //float rotationX = 90f - Vector3.Angle(Vector3.up, hitNormal);

        //transform.rotation = Quaternion.Euler(isRedPortal? rotationX : -rotationX, rotationY, 0);
    }
    void IsConnected()                                          //포탈이 둘다 열리지 않을경우 연결되지않음을 나타내는 이미지 출력
    {
        bool connected = otherPotal.gameObject.activeSelf;
        notConnectImg.enabled = !connected;
        otherPotal.notConnectImg.enabled = !connected;
    }
    //private void OnTriggerEnter(Collider other)
    //{
    //    other.transform.SetParent(transform);
    //}
    private void OnTriggerStay(Collider other)                              //포탈간 이동에 관한 코드 !!!!!
    {
        if (((1 << other.gameObject.layer) & canTelefortLayerMask) == 0)            //이동가능한 물체 레이어 검사
            return;

        if (other.TryGetComponent<Cube>(out Cube cube) && cube.IsPickedUp)          //e로 집은 큐브는 제외
            return;

        if (plane.GetDistanceToPoint(other.transform.position) >= 0)                //아까 설정한 포탈 평면과 이동될 물체의 좌표의 거리가 0 미만일때 다른포탈로 전송
            return;

        other.transform.SetParent(transform);                                           //   이동시킬 물체를 이 포탈의 자식으로설정
        other.transform.SetParent(otherPotal.transform, false);                         // local Position, local rotation을 고정시킨채로 다른포탈의 자식으로 재설정(==다른포탈 장소로 순간이동)
        Vector3 localPos = other.transform.localPosition;
        other.transform.localPosition = new Vector3(-localPos.x, localPos.y, -localPos.z);  //(포탈에서 나오는 방향으로) y축으로 180도 회전시킨 position로 조정


        Vector3 localEuler = other.transform.localRotation.eulerAngles;                     //(포탈에서 나오는 방향으로) y축으로 180도 회전시킨 rotation으로 조정
        localEuler.y = localEuler.y + 180f;
        other.transform.localRotation = Quaternion.Euler(localEuler);


        Rigidbody rb = other.GetComponent<Rigidbody>();                                     //(포탈에서 나오는 방향으로) y축으로 180도 회전시킨 velocity를 가지도록 조정
        Vector3 velocity = rb.velocity;
        velocity = transform.InverseTransformDirection(velocity);
        velocity = new Vector3(-velocity.x, velocity.y, -velocity.z);
        velocity = otherPotal.transform.TransformDirection(velocity);
        velocity += (otherPotal.transform.forward * aditionalPortalExitSpeed);              //나올때 사출속도 추가해줌
        rb.velocity = velocity;

        other.transform.SetParent(null);

        /*
        //=========================================================================================== 폐기된 코드들
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

    //void Telefort(Collider other)
    //{
    //    if (plane.GetDistanceToPoint(other.transform.position) >= 0)
    //        return;

    //    other.transform.SetParent(transform);
    //    other.transform.SetParent(otherPotal.transform, false);
    //    Vector3 localPos = other.transform.localPosition;
    //    other.transform.localPosition = new Vector3(-localPos.x, localPos.y, -localPos.z);


    //    Vector3 localEuler = other.transform.localRotation.eulerAngles;
    //    localEuler.y = localEuler.y + 180f;
    //    other.transform.localRotation = Quaternion.Euler(localEuler);


    //    Rigidbody rb = other.GetComponent<Rigidbody>();
    //    Vector3 velocity = rb.velocity;
    //    velocity = transform.InverseTransformDirection(velocity);
    //    velocity = new Vector3(-velocity.x, velocity.y, -velocity.z);
    //    velocity = otherPotal.transform.TransformDirection(velocity);
    //    rb.velocity = velocity;

    //    other.transform.SetParent(null);
    //}

    public Vector3 LaserPosition(Vector3 hitPoint)                          //들어온 레이저의 좌표(레이저를 맞은 좌표)를 이용해 (다른쪽 포탈에서)나올 레이저 시작 좌표를 반환 
    {
        Vector3 localHitPoint = transform.InverseTransformPoint(hitPoint);

        if (!otherPotal.gameObject.activeInHierarchy)
        {
            return Vector3.zero;
        }
        localHitPoint = new Vector3(-localHitPoint.x,localHitPoint.y,localHitPoint.z);
        Vector3 newStartPosition = otherPotal.transform.TransformPoint(localHitPoint) + (otherPotal.transform.forward * 0.01f);

        return newStartPosition;

    }
    
    public Vector3 LaserDirection(Vector3 hitPoint,Vector3 startPosition)       //들어오 레이저의 방향을 이용해 (다른포탈에서)나올 레이저의 방향을 반환
    {
        Vector3 laserDirection = hitPoint - startPosition;
        laserDirection = transform.InverseTransformDirection(laserDirection);
        laserDirection = new Vector3(-laserDirection.x, laserDirection.y, -laserDirection.z);

        laserDirection = otherPotal.transform.TransformDirection(laserDirection);

        return laserDirection;
    }
    public Vector3 LaserNormalPosition(Vector3 hitPoint)                //===================  만일 들어가는 각도와 위치에 상관없이 포탈 중앙에서 레이저를 나오게하고싶다면 아래 코드
    {
        return transform.position + transform.forward * 0.1f;
    }

    public Vector3 LaserNormalDirection(Vector3 hitPoint, Vector3 startPosition)
    {
        return transform.forward;
    }


}
