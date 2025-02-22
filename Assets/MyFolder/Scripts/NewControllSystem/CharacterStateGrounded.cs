using UnityEngine;
using UnityEngine.Windows;

public class CharacterStateGrounded: CharacterStateBase
{
    private float _speed;
    private Vector2 _moveInput;

    public CharacterStateGrounded(CharacterStateMachine stateMachine, CharacterState characterState, Rigidbody2D rb, InputHandler input, float speed)
        : base(stateMachine, characterState, rb, input)
    {
        _speed = speed;
    }

    public override void Enter()
    {
        Debug.Log("Entered Grounded State");
    }

    public override void HandleInput(Vector2 input)
    {
        _moveInput = input;
        if (_input.IsJumpPressed)
        {
            _stateMachine.ChangeState(_stateMachine.JumpingState);
        }
    }

    public override void Update()
    {
        if (!_characterState.IsGrounded)
        {
            _stateMachine.ChangeState(_stateMachine.AirborneState);
        }

        if (_characterState.IsOnRope)
        {
            _stateMachine.ChangeState(_stateMachine.RopeState);
        }
    }

    public override void FixedUpdate()
    {
        _rb.linearVelocity = new Vector2(_moveInput.x * _speed, _rb.linearVelocity.y);
        if ((_moveInput.x > 0 && !_characterState.IsFacingRight) || (_moveInput.x < 0 && _characterState.IsFacingRight))
        {
            _characterState.FlipDirection();
        }
    }
}
