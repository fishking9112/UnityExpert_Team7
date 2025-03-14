using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cube_Rayser : MonoBehaviour
{
    public GameObject Raybody;
    public GameObject ScaleDistance;
    public GameObject RayResult;

    private bool ChkRayser = false;
    private float maxDistance = 200f;
    private void Update()
    {
        if (ChkRayser)
        {
            //여기서  z축 방향으로 layser 쏘기
            RaycastHit hit;
            ScaleDistance.SetActive(true);

            if (Physics.Raycast(transform.position, transform.forward, out hit, maxDistance))
            {
                int layserIndex = hit.collider.gameObject.layer;
                string layerName = LayerMask.LayerToName(layserIndex);
                //Debug.Log("Hit object layer: " + layerName + " (index: " + layserIndex + ")");
                Debug.DrawRay(transform.position, transform.forward * hit.distance, Color.yellow);

                Vector3 middlePosition = transform.position + (hit.point - transform.position) / 2;

                ScaleDistance.transform.position = middlePosition;
                ScaleDistance.transform.localScale = new Vector3(0.1f, hit.distance, 0.1f);
            }
        }
        else
        {
            ScaleDistance.SetActive(false);

        }


    }

    public void ChkRayserLayser() => ChkRayser = true;
    public void ChkOutRayserLayser() => ChkRayser = false;



}
