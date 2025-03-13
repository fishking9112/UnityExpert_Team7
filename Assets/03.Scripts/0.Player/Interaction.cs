using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// 카메라 중심에서 찍히는 물체를 판단하기 위한 스크립트.
/// </summary>

public class Interaction : MonoBehaviour
{
    [SerializeField] private Transform equipCamera;
    [SerializeField] private Transform holdTransform; // 물체를 들고 있을 위치
    private IPickable heldObject = null;
    private IInteractable interactable;

    public float checkRate = 0.05f; // 검사 주기
    private float _lastCheckTime;   // 마지막 체크한 시간
    public float maxCheckDist;      // 레이 거리

    public LayerMask layerMask;     // 검사할 레이어 마스크

    //    public GameObject curInteractionOBJ;    // 현재 찍은 OBJ

    private Camera _camera;                 // 카메라 중심을 위한 카메라

    void Start()
    {
        _camera = Camera.main;
    }

    void Update()
    {
        tryInteract();
    }

    private void tryInteract()
    {
        //일정 시간마다 Ray 쏘기
        if (Time.time - _lastCheckTime > checkRate)
        {
            _lastCheckTime = Time.time;

            Ray ray = _camera.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2));
            RaycastHit hit;

            //카메라 중점에서 레이 발사 해서 충돌 했으면 ?
            if (Physics.Raycast(ray, out hit, maxCheckDist))
            {
                // ray를 쏴서 interable 가져오기
                interactable = hit.collider.GetComponent<IInteractable>();

                //interable 의 존재하고 && 상호작용이 가능한 상태라면
                if (interactable != null && interactable.CanInteract(this))
                {
                    //담는역활만하고 
                    // 튜토리얼 보여주기 

                }
            }
            else
            {
                interactable = null;
            }
        }
    }

    public void OnInteract(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started && interactable != null)
        {
            //상호작용 실행
            interactable.Interact(this);
            Debug.Log(interactable.GetInteractionPrompt());
        }
    }
    public Transform GetEquipCameraTransform()
    {

        return equipCamera; 
    }
    public Transform GetHoldTransform()
    {
        return holdTransform;
    }
    public bool CanPickUpObject()
    {
        return heldObject == null;
    }
    public void SetHeldObject(IPickable pickable)
    {
        heldObject = pickable;
    }
    public void ClearHeldObject()
    {
        heldObject = null;
    }
}
