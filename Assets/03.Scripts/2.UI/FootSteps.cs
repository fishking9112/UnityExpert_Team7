using UnityEngine;
using UnityEngine.UI;

public class FootSteps : MonoBehaviour
{
    public AudioClip footstepClip;
    public float footstepRate = 0.5f;

    private Rigidbody _rigidbody;
    private Vector3 _moveInput;
    private float _lastFootstepTime;
    public LayerMask groundLayer;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");
        _moveInput = new Vector3(moveX, 0, moveZ).normalized;

        if (_moveInput.magnitude > 0.1f && Time.time - _lastFootstepTime > footstepRate && IsGrounded())
        {
            _lastFootstepTime = Time.time;
            PlayFootstepSound();
        }
    }

    private void FixedUpdate()
    {
        _rigidbody.MovePosition(_rigidbody.position + _moveInput * Time.fixedDeltaTime);
    }

    private void PlayFootstepSound()
    {
        if (footstepClip != null)
        {
            SoundManager.instance.PlaySFX(footstepClip, 1f);
        }
    }

    private bool IsGrounded()
    {
        return Physics.Raycast(transform.position, Vector3.down, 1.1f, groundLayer);
    }
}
