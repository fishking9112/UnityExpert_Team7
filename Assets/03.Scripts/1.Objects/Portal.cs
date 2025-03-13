using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.HID;

public class Portal : MonoBehaviour
{
    [SerializeField] Portal otherPotal;
    Plane plane;
    [SerializeField] bool isRedPortal;
    Collider wallCollider;

    Vector3 forward;
    Vector3 right;
    Vector3 up;
    
    public void SummonPortal(Vector3 hitPoint, Vector3 hitNormal, Collider hitCollider)
    {


        this.gameObject.SetActive(true);
        if(wallCollider != null)
        {
            wallCollider.enabled = true;
        }
        wallCollider = hitCollider;
        wallCollider.enabled = false;

        forward = isRedPortal? hitNormal : -hitNormal;
        right = Vector3.Cross(Vector3.up, forward);
        up = Vector3.Cross(forward, right);

        transform.rotation = Quaternion.LookRotation(forward,up);

        //float rotationY = Vector3.SignedAngle(Vector3.forward * (isRedPortal ? 1f: -1f), hitNormal, Vector3.up);
        //float rotationX = 90f - Vector3.Angle(Vector3.up, hitNormal);

        transform.position = hitPoint;
        //transform.rotation = Quaternion.Euler(isRedPortal? rotationX : -rotationX, rotationY, 0);
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
        other.transform.rotation = quaternion * other.transform.rotation;
        //other.transform.rotation = Quaternion.LookRotation(forward, up);
        //rb.velocity = Quaternion.LookRotation(forward, up)*rb.velocity;

        rb.velocity = quaternion * rb.velocity;
        enterPosition = otherPotal.transform.rotation * enterPosition;
        other.transform.position = otherPotal.transform.position + enterPosition + ((isRedPortal ? -otherPotal.transform.forward : otherPotal.transform.forward) * 0.1f);
    }
}
