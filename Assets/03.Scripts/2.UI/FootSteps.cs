using UnityEngine;

/// <summary>
/// 캐릭터 발자국 소리 테스트
/// </summary>

public class FootSteps : MonoBehaviour
{
    public AudioClip[] footStepClips;
    private AudioSource _audioSource;
    private Rigidbody _rigidbody;
    public float footStepThreshold;
    public float footStepRate;
    private float _footStepTime;
    public LayerMask groundLayer;

    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (IsGrounded())
        {
            if (_rigidbody.velocity.magnitude > footStepThreshold)
            {
                if (Time.time - _footStepTime > footStepRate)
                {
                    _footStepTime = Time.time;
                    _audioSource.PlayOneShot(footStepClips[Random.Range(0, footStepClips.Length)]);
                }
            }
        }
    }
    private bool IsGrounded()
    {
        return Physics.Raycast(transform.position, Vector3.down, 1.1f, groundLayer);
    }
}
