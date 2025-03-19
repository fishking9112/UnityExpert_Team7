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

    bool isPause;
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

    void CheckPotalAble()           //포탈이 소환가능한지 여부를 판단  ==>>  조건 : <BasePortalAble>스크립트 소지, 해당 물체(벽)에 이미 열려있는 다른 종류(색) 포탈 없어야함
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
        if (!canShotPortal || isPause)                  //포탈건 미소지,일시정지상태 일때 발사금지
            return;
        if(context.performed)
        {
            if (shootSound != null)
            {
                SoundManager.instance.PlaySFX(shootSound);
            }
            gunScript.ShootAnimation(true);

            if (canShotRedPortal)                       //포탈 소환 가능 조건검사
            {
                redWall?.SetMainCollider(true);         //원래 해당포탈(red)이 소환되있던 벽의 콜라이더 정상화
                redWall = wall;                         //소환될 벽 저장
                redWall.SetMainCollider(false);         //소환될 벽 콜라이더 변형(포탈만한 콜라이더 구멍생성)
                Vector3 summonPos = redWall.SummonPortal(hit.point);    //벽에서 보정된 소환가능 위치 반환 (모서리에 쏘면 안쪽으로 자동 보정됨)
                redPortal.SummonPortal(summonPos, hit.normal);          //포탈소환
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
    public void OnShotBluePortal(InputAction.CallbackContext context)   //위(red)와 같음
    {
        if (!canShotPortal || isPause)
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
    public void SetPause(bool value)
    {
        isPause = value;
    }
    public void ActivatePortalGun(bool value)       //포탈건 집어야 포탈 발사가능
    {
         canShotPortal = value;
    }
}
