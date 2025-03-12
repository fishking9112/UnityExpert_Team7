using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
    [SerializeField] Portal otherPotal;
    Plane plane;
    private void OnEnable()
    {
        plane = new Plane(transform.forward,transform.position);
    }
    private void OnTriggerStay(Collider other)
    {
        if(plane.GetDistanceToPoint(other.transform.position) <= 0)
        {
            other.transform.rotation = Quaternion.Euler(0,180f,0) * other.transform.rotation;
            Quaternion quaternion = Quaternion.Inverse(transform.rotation)*otherPotal.transform.rotation;
            other.transform.rotation = quaternion*other.transform.rotation;

            other.transform.position = otherPotal.transform.position+otherPotal.transform.forward*0.01f;
        }
    }
}
