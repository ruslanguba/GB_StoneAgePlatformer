using System.Collections;
using UnityEngine;
using UnityEngine.Windows;

public class CharacterMovement : MonoBehaviour, IMovable
{
    [SerializeField] private float _speed = 5;
    [SerializeField] private float _airSpeed = 3;
    private Rigidbody2D _rigidbody2D;
    private Vector2 _moveInput;
    private CharacterState _characterState;

    void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _characterState = GetComponent<CharacterState>();
    }

    private void FixedUpdate()
    {
        Move();
    }
    //public void SetMoveInput(Vector2 moveInput)
    //{
    //    _moveInput = moveInput;
    //}

    private void Move()
    {
        if (_characterState.IsOnRope || _characterState.IsOnLedge) return;

        if (_characterState.IsGrounded)
        {
            _rigidbody2D.linearVelocity = new Vector2(_moveInput.x * _speed, _rigidbody2D.linearVelocity.y);
        }
        else
        {
            _rigidbody2D.AddForce(new Vector2(_moveInput.x * _airSpeed, 0), ForceMode2D.Force);
        }

        if ((_moveInput.x > 0 && !_characterState.IsFacingRight) || (_moveInput.x < 0 && _characterState.IsFacingRight))
        {
            _characterState.FlipDirection();
        }
    }

    public void HandleMove(Vector2 direction)
    {
        _moveInput = direction;
    }
}
