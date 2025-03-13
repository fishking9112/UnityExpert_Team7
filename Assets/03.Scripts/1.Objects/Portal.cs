using UnityEngine;

public class Portal : MonoBehaviour
{
    [SerializeField] Portal otherPotal;
    Plane plane;
    [SerializeField] bool isRedPortal;
    Collider wallCollider;

    

    
    
    
    
    public void SummonPortal(Vector3 hitPoint, Vector3 hitNormal, Collider hitCollider)
    {


        this.gameObject.SetActive(true);
        if(wallCollider != null)
        {
            wallCollider.enabled = true;
        }
        wallCollider = hitCollider;
        wallCollider.enabled = false;

        Vector3 forward = isRedPortal? hitNormal : -hitNormal;
        Vector3 right = Vector3.Cross(Vector3.up, forward);
        Vector3 up = Vector3.Cross(forward, right);

        transform.rotation = Quaternion.LookRotation(forward,up);

        //float rotationY = Vector3.SignedAngle(Vector3.forward * (isRedPortal ? 1f: -1f), hitNormal, Vector3.up);
        //float rotationX = 90f - Vector3.Angle(Vector3.up, hitNormal);

        transform.position = hitPoint;
        //transform.rotation = Quaternion.Euler(isRedPortal? rotationX : -rotationX, rotationY, 0);
    }

    private void OnTriggerEnter(Collider other)
    {
        other.transform.SetParent(transform);
    }
    private void OnTriggerStay(Collider other)
    {
        plane = new Plane(transform.forward, transform.position);


        if (isRedPortal && plane.GetDistanceToPoint(other.transform.position) > 0)
            return;
        if (!isRedPortal && plane.GetDistanceToPoint(other.transform.position) < 0)
            return;

       

        Rigidbody rb = other.GetComponent<Rigidbody>();
        Vector3 enterPosition = other.transform.position - transform.position;
        enterPosition = enterPosition - (Vector3.Dot(enterPosition, transform.forward) * (isRedPortal ? transform.forward : -transform.forward));

        Quaternion quaternion = Quaternion.Inverse(transform.rotation) * otherPotal.transform.rotation;
        Debug.Log($"{quaternion}");

        //Quaternion curLocalRotation = other.transform.localRotation;
        //Vector3 curLocalPosition = other.transform.localPosition;

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
        other.transform.position += (isRedPortal? -otherPotal.transform.forward : otherPotal.transform.forward) * 1f;
        //other.transform.localRotation = curLocalRotation;

        //other.transform.rotation = Quaternion.Euler(other.transform.rotation.x + otherPotal.transform.rotation.x - transform.rotation.x, other.transform.rotation.y + otherPotal.transform.rotation.y - transform.rotation.y + 180f, other.transform.rotation.z + otherPotal.transform.rotation.z - transform.rotation.z);

        //other.transform.rotation = quaternion * other.transform.rotation;

        rb.velocity = quaternion * rb.velocity;
        //enterPosition = otherPotal.transform.rotation * enterPosition;
        //other.transform.position = otherPotal.transform.position + enterPosition + ((isRedPortal ? -otherPotal.transform.forward : otherPotal.transform.forward) * 0.1f);
    }
    private void OnTriggerExit(Collider other)
    {
        other.transform.SetParent (null);
    }
}
