using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
    [SerializeField] Portal otherPotal;
    Plane plane;
    [SerializeField] bool isRedPortal;
    private void OnEnable()
    {
        
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
        enterPosition = enterPosition - (Vector3.Dot(enterPosition, Vector3.forward) * (isRedPortal ? Vector3.forward : -Vector3.forward));

        Quaternion quaternion = Quaternion.Inverse(transform.rotation) * otherPotal.transform.rotation;
        other.transform.rotation = quaternion * other.transform.rotation;


        rb.velocity = quaternion * rb.velocity;
        enterPosition = otherPotal.transform.rotation * enterPosition;
        other.transform.position = otherPotal.transform.position + enterPosition + ((isRedPortal ? -otherPotal.transform.forward : otherPotal.transform.forward) * 0.1f);
    }
}
