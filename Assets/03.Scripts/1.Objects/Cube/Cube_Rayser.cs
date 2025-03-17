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
    private GameObject lastHitObj = null;
    private void Update()
    {
        if (ChkRayser)
        {
            //여기서  z축 방향으로 layser 쏘기
            RaycastHit hit;
            GameObject currentHitObj = null;

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


                if (layserIndex == LayerMask.NameToLayer("LayserBtn"))
                {
                    currentHitObj = hit.collider.gameObject;
                    hit.collider.GetComponent<Button>().ChkedPress();
                }
            }

            if (lastHitObj != null && lastHitObj != currentHitObj)
            {
                Button lastbtnRayser = lastHitObj.GetComponent<Button>();
                if (lastbtnRayser != null)
                {
                    lastbtnRayser.ChkOutPress();
                }
            }

            lastHitObj = currentHitObj;
        }
        else
        {
            Debug.DrawRay(transform.position, transform.forward * maxDistance, Color.yellow);
            
            ScaleDistance.SetActive(false);
        }


    }

    public void ChkRayserLayser() => ChkRayser = true;
    public void ChkOutRayserLayser() => ChkRayser = false;



}
