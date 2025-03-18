using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rayser_Portal : MonoBehaviour
{
    //public GameObject Raybody;
    public GameObject ScaleDistance;
    //public GameObject RayResult;

    private bool ChkRayser = false;
    private float maxDistance = 200f;
    private GameObject lastHitObj = null;
    private Portal portal;

    private Vector3 portalPosition = Vector3.zero;
    private Vector3 portalDirection = Vector3.zero;

    private Vector3 newPortalPosition = Vector3.zero;
    private Vector3 newPortalDirection = Vector3.zero;
    private void Start()
    {
        portal = GetComponent<Portal>();


    }
    private void Update()
    {
        GameObject currentHitObj = null;
        newPortalPosition = portal.LaserPosition(portalPosition);
        newPortalDirection = portal.LaserDirection(portalPosition, portalDirection);

        if (portalPosition != Vector3.zero && newPortalPosition != Vector3.zero)
        {
            //ScaleDistance.transform.rotation = Quaternion.Euler(-90f, 0f, 0f);
            //포탈위치에 레이저 쏴주기 
            RaycastHit hit;

            ScaleDistance.SetActive(true);
            //if (Physics.Raycast(newPortalPosition, newPortalDirection, out hit, maxDistance))
            if (Physics.Raycast(newPortalPosition, transform.forward, out hit, maxDistance))
            {
                int index_layer = hit.collider.gameObject.layer;
                //Debug.DrawRay(newPortalPosition, newPortalDirection * hit.distance, Color.yellow);
                Debug.DrawRay(newPortalPosition, transform.forward * hit.distance, Color.yellow);

                Vector3 middlePosition = newPortalPosition + (hit.point - newPortalPosition) / 2;
                ScaleDistance.transform.position = middlePosition;
                ScaleDistance.transform.localScale = new Vector3(0.1f, hit.distance, 0.1f);

                // 레이저가 히트 지점을 향하도록 회전
                //ScaleDistance.transform.LookAt(hit.point);
                //ScaleDistance.transform.localEulerAngles += new Vector3(-90f, 0f, 0f);

                if (index_layer == LayerMask.NameToLayer("LayserCube"))
                {
                    currentHitObj = hit.collider.gameObject;
                    hit.collider.GetComponent<Cube_Rayser>().ChkRayserLayser();
                }
                else if (index_layer == LayerMask.NameToLayer("LayserBtn"))
                {
                    currentHitObj = hit.collider.gameObject;
                    hit.collider.GetComponent<ButtonObj>().ChkedPress();
                }

                if (lastHitObj != null && lastHitObj != currentHitObj)
                {
                    ButtonObj lastbtnRayser = lastHitObj.GetComponent<ButtonObj>();
                    if (lastbtnRayser != null)
                    {
                        lastbtnRayser.ChkOutPress();
                    }
                }
            }
            else
            {
                Debug.DrawRay(newPortalPosition, transform.forward * maxDistance, Color.yellow);

                Vector3 endPoint = newPortalPosition + transform.forward * maxDistance;
                Vector3 midPotnt = (newPortalPosition + endPoint) / 2;
                ScaleDistance.transform.position = midPotnt;
                ScaleDistance.transform.localScale = new Vector3(0.1f, maxDistance, 0.1f);
                // 레이저가 히트 지점을 향하도록 회전
                //ScaleDistance.transform.LookAt(hit.point);
                //ScaleDistance.transform.localEulerAngles += new Vector3(-90f, 0f, 0f);
            }
        }
        else
        {
            ScaleDistance.SetActive(false);
        }

        if (lastHitObj !=null && lastHitObj != currentHitObj)
        {
            Cube_Rayser cube_Rayser = lastHitObj.GetComponent<Cube_Rayser>();
            if (cube_Rayser != null)
            {
                cube_Rayser.ChkOutRayserLayser();
            }
        }

        lastHitObj=currentHitObj;

    }

    public void SetPortalPosition(Vector3 _position)
    {
        portalPosition = _position;
    }
    public void SetPotalDirection(Vector3 _Direction)
    {
        portalDirection = _Direction;
    }
    public void SetPotalDirectioninit()
    {
        portalPosition = Vector3.zero;

    }
}
