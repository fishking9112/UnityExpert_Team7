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
        plane = new Plane(transform.forward,transform.position);
    }
    private void OnTriggerStay(Collider other)
    {
        if (isRedPortal && plane.GetDistanceToPoint(other.transform.position) > 0)
            return;
        if (!isRedPortal && plane.GetDistanceToPoint(other.transform.position) < 0)
            return;

        Rigidbody rb = other.GetComponent<Rigidbody>();
        Vector3 enterPosition = other.transform.position - transform.position;

        Quaternion quaternion = Quaternion.Inverse(transform.rotation) * otherPotal.transform.rotation;
        other.transform.rotation = quaternion * other.transform.rotation;


        rb.velocity = quaternion * rb.velocity;
        enterPosition = otherPotal.transform.rotation * enterPosition;
        other.transform.position = otherPotal.transform.position + enterPosition + (isRedPortal ? -otherPotal.transform.forward * 1f : otherPotal.transform.forward * 1f);
    }
}
