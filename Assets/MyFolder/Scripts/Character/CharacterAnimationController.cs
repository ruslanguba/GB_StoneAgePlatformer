using UnityEngine;

public class CharacterAnimationController : MonoBehaviour
{
    private Animator _animator;
    private Rigidbody2D _rb;
    private Character _character; // ������ �� ������ ��������

    private void OnEnable()
    {
        _character.OnJump += JampAnimationPlay;
    }

    private void OnDisable()
    {
        _character.OnJump -= JampAnimationPlay;
    }

    private void Awake()
    {
        if(GetComponentInChildren<Animator>() != null)
        {
            _animator = GetComponentInChildren<Animator>();
        }
        _rb = GetComponent<Rigidbody2D>();
        _character = GetComponent<Character>(); // ���� ���� ���� ����� ��������
    }

    private void Update()
    {
        if (_animator != null)
        {
            _animator.SetFloat("Speed", Mathf.Abs(_rb.linearVelocity.x));
        }
    }

    private void JampAnimationPlay()
    {
        _animator.SetTrigger("Jump");
    }
}
