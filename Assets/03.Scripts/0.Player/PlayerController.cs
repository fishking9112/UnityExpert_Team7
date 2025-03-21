using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.XR;
using UnityEngine.UI;
using static UnityEngine.UI.Image;

public class PlayerController : MonoBehaviour
{
    Rigidbody _rigidbody;
    [SerializeField] Transform _cameraContainer;

    Vector2 _curMoveInput;
    Vector2 _mouseDelta;
    float _camCurXRot;

    [SerializeField] float moveSpeed;
    [SerializeField] float jumpPower;
    [SerializeField] float lookSensitivity;
    [SerializeField] float minXLook,maxXLook;


    [SerializeField] LayerMask groundLayerMask;
    [SerializeField] Image pauseImg;

    [SerializeField]bool _isPause;
    [SerializeField]bool _isJump;
    [SerializeField] bool _isGround;

    [SerializeField] float standSpeed;

    PortalGun portalGun;
    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        portalGun = GetComponentInChildren<PortalGun>();
    }
    private void Start()
    {
        _isPause = false;
        Cursor.lockState = CursorLockMode.Locked;
    }
    private void Update()
    {

    }
    private void FixedUpdate()
    {
        if (transform.rotation.x != 0 || transform.rotation.z != 0)
        {
            transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(0, transform.rotation.eulerAngles.y, 0), standSpeed);
        }
        Move();
        
        _rigidbody.useGravity = (_isJump);      //점프상태가 아니면 중력 끔 ==>> 경사로 미끄러짐 방지
        _isGround = IsGround();
        _isJump = !_isGround;
    }
    private void LateUpdate()
    {
        if (!_isPause)
        {
            RotateLook();
        }
        
    }
    public void OnLook(InputAction.CallbackContext context)
    {
        _mouseDelta = context.ReadValue<Vector2>();
    }
    void RotateLook()
    {
        _camCurXRot += _mouseDelta.y * lookSensitivity;
        _camCurXRot = Mathf.Clamp(_camCurXRot, minXLook, maxXLook);
        _cameraContainer.localEulerAngles = new Vector3(-_camCurXRot, 0, 0);

        transform.eulerAngles += new Vector3(0, _mouseDelta.x * lookSensitivity, 0);
    }
    
    public void OnMove(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            _curMoveInput = context.ReadValue<Vector2>();
        }
        else if (context.canceled)
        {
            _curMoveInput = Vector2.zero;
        }
    }
    public void Move()
    {

        Vector3 moveDirection = transform.forward * _curMoveInput.y + transform.right * _curMoveInput.x;


        if (_isJump)                //점프상태일때 방향키로 이동안됨, 0.08만큼 살짝만 조정됨
        {
            _rigidbody.velocity += new Vector3(moveDirection.normalized.x, 0, moveDirection.normalized.z) * 0.08f;      
            return;
        }
        moveDirection *= moveSpeed;
        moveDirection.y =_rigidbody.velocity.y;

        _rigidbody.velocity = moveDirection;
    }
    public void OnJump(InputAction.CallbackContext context)
    {
        if (!_isJump)
        {
            _isJump = true;
            Vector3 velocity = _rigidbody.velocity;
            _rigidbody.velocity = new Vector3(velocity.x, jumpPower, velocity.z);
        }
        
        //if (!_isJump)
        //{
            
        //    _isJump = true;
        //}
        
        
    }

    public void OnESC(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            Time.timeScale = _isPause ? 1f : 0f;                //일시정시 상태시 시간정지
            pauseImg.gameObject.SetActive(!_isPause);           //일시정지창 활성화
            Cursor.lockState = _isPause ? CursorLockMode.Locked : CursorLockMode.None;  //마우스 락 활성/비활성 화
            portalGun.SetPause(!_isPause);                                  //시간정지되도 시야 돌릴수있기때문에 시야 회전 잠금을 위한 불값조정
            _isPause = !_isPause;
            
        }

    }

    bool IsGround()
    {
        Ray[] rays = new Ray[4]
        {
            new Ray(transform.position + (transform.forward * 0.29f) +(-transform.up*0.5f),Vector3.down),
            new Ray(transform.position + (-transform.forward * 0.29f) +(-transform.up*0.5f),Vector3.down),
            new Ray(transform.position + (transform.right * 0.29f) +(-transform.up*0.5f),Vector3.down),
            new Ray(transform.position + (-transform.right * 0.29f) +(-transform.up*0.5f),Vector3.down),
        };

        for (int i = 0; i < rays.Length; i++)
        {
            if (Physics.Raycast(rays[i], 0.40f, groundLayerMask))
            {
                return true;
            }
        }
        return false;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position + (transform.forward * 0.29f) + (-transform.up * 0.5f), Vector3.down * 0.40f);
        Gizmos.DrawRay(transform.position + (-transform.forward * 0.29f) + (-transform.up * 0.5f), Vector3.down * 0.40f);
        Gizmos.DrawRay(transform.position + (transform.right * 0.29f) + (-transform.up * 0.5f), Vector3.down * 0.40f);
        Gizmos.DrawRay(transform.position + (-transform.right * 0.29f) + (-transform.up * 0.5f), Vector3.down * 0.40f);
    }
}
