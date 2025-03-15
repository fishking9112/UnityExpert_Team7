using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PortalGun : MonoBehaviour
{
    [SerializeField] LayerMask canShotPotalLayerMask;

    Portal redPortal;
    Portal bluePortal;

    [SerializeField] GameObject redPortalPrefab;
    [SerializeField] GameObject bluePortalPrefab;

    BasePortalAble redWall;
    BasePortalAble blueWall;

    private void Awake()
    {
        if(redPortal == null)
        {
            redPortal = Instantiate(redPortalPrefab).GetComponent<Portal>();
        }
        if (bluePortal == null)
        {
            bluePortal = Instantiate(bluePortalPrefab).GetComponent<Portal>();
        }
        redPortal.SetOtherPortal(bluePortal);
        bluePortal.SetOtherPortal(redPortal);
        redPortal.player = GetComponentInParent<Player>();
        bluePortal.player = GetComponentInParent<Player>();

        redPortal.gameObject.SetActive(false);
        bluePortal.gameObject.SetActive(false);
    }
    public void OnShotRedPortal(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            Ray ray = Camera.main.ScreenPointToRay(new Vector2(Screen.width / 2, Screen.height / 2));

            if (Physics.Raycast(ray, out RaycastHit hit, 2000f, canShotPotalLayerMask))
            {
                if (hit.transform.TryGetComponent<BasePortalAble>(out BasePortalAble wall) && wall != blueWall)
                {
                    redWall?.SetMainCollider(true);
                    redWall = wall;
                    redWall.SetMainCollider(false);
                    Vector3 summonPos = redWall.SummonPortal(hit.point);
                    redPortal.SummonPortal(summonPos, hit.normal);
                }
                //hit.point 에서 불꽃이펙트 실행
            }
        }
        
    }
    public void OnShotBluePortal(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            Ray ray = Camera.main.ScreenPointToRay(new Vector2(Screen.width / 2, Screen.height / 2));

            if (Physics.Raycast(ray, out RaycastHit hit, 2000f, canShotPotalLayerMask))
            {
                if(hit.transform.TryGetComponent<BasePortalAble>(out BasePortalAble wall) && wall != redWall)
                {
                    blueWall?.SetMainCollider(true);
                    blueWall = wall;
                    blueWall.SetMainCollider(false);
                    Vector3 summonPos = blueWall.SummonPortal(hit.point);
                    bluePortal.SummonPortal(summonPos, hit.normal);
                }
                //hit.point 에서 불꽃이펙트 실행
            }
        }
        
    }
}
