using System.Collections;
using System.Collections.Generic;
using TMPro.EditorUtilities;
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

    public CrossHair crossHair;
    [SerializeField] AudioClip shootSound;

    public Ray ray;
    Gun gunScript;
    RaycastHit hit;
    BasePortalAble wall;

    bool canShotPortal;
    bool canShotRedPortal;
    bool canShotBluePortal;

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

        canShotPortal = false;

    }
    private void Start()
    {
        //Find 하나만 할게요
        crossHair = GameObject.Find("GameMenuCanvas").GetComponent<GameMenuController>().crossHair;
    }
    private void Update()
    {
        CheckPotalAble();
        crossHair.CanShotRed(canShotRedPortal);
        crossHair.CanShotBlue(canShotBluePortal);
    }
    public void SetGun(Gun gun)
    {
        gunScript = gun;
    }

    void CheckPotalAble()
    {
        ray = Camera.main.ScreenPointToRay(new Vector2(Screen.width / 2, Screen.height / 2));
        if (Physics.Raycast(ray, out hit, 2000f, canShotPotalLayerMask) && hit.transform.TryGetComponent<BasePortalAble>(out wall))
        {
            canShotRedPortal = wall != blueWall;
            canShotBluePortal = wall != redWall;
        }
        else
        {
            canShotRedPortal = false;
            canShotBluePortal = false;
        }
    }

    public void OnShotRedPortal(InputAction.CallbackContext context)
    {
        if (!canShotPortal)
            return;
        if(context.performed)
        {
            if (shootSound != null)
            {
                SoundManager.instance.PlaySFX(shootSound);
            }
            gunScript.ShootAnimation(true);

            if (canShotRedPortal)
            {
                redWall?.SetMainCollider(true);
                redWall = wall;
                redWall.SetMainCollider(false);
                Vector3 summonPos = redWall.SummonPortal(hit.point);
                redPortal.SummonPortal(summonPos, hit.normal);
            }
            
        }


        //if (context.performed)
        //{
        //    if (Physics.Raycast(ray, out RaycastHit hit, 2000f, canShotPotalLayerMask))
        //    {
        //        if (hit.transform.TryGetComponent<BasePortalAble>(out BasePortalAble wall) && wall != blueWall)
        //        {
        //            redWall?.SetMainCollider(true);
        //            redWall = wall;
        //            redWall.SetMainCollider(false);
        //            Vector3 summonPos = redWall.SummonPortal(hit.point);
        //            redPortal.SummonPortal(summonPos, hit.normal);
        //        }
        //        //hit.point 에서 불꽃이펙트 실행
        //    }
        //}

    }
    public void OnShotBluePortal(InputAction.CallbackContext context)
    {
        if (!canShotPortal)
            return;

        if (context.performed)
        {
            if(shootSound != null)
            {
                SoundManager.instance.PlaySFX(shootSound);
            }
                
            gunScript.ShootAnimation(false);

            if (canShotBluePortal)
            {
                blueWall?.SetMainCollider(true);
                blueWall = wall;
                blueWall.SetMainCollider(false);
                Vector3 summonPos = blueWall.SummonPortal(hit.point);
                bluePortal.SummonPortal(summonPos, hit.normal);
            }
            
        }

        //if (context.performed)
        //{
        //    if (Physics.Raycast(ray, out RaycastHit hit, 2000f, canShotPotalLayerMask))
        //    {
        //        if(hit.transform.TryGetComponent<BasePortalAble>(out BasePortalAble wall) && wall != redWall)
        //        {
        //            blueWall?.SetMainCollider(true);
        //            blueWall = wall;
        //            blueWall.SetMainCollider(false);
        //            Vector3 summonPos = blueWall.SummonPortal(hit.point);
        //            bluePortal.SummonPortal(summonPos, hit.normal);
        //        }
        //        //hit.point 에서 불꽃이펙트 실행
        //    }
        //}
        
    }

    public void ActivatePortalGun(bool value)
    {
         canShotPortal = value;
    }
}
