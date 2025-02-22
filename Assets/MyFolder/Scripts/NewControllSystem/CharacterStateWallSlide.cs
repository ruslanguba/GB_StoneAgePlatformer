using UnityEngine;
using UnityEngine.Windows;

public class CharacterStateWallSlide : CharacterStateBase
{
    private float _slideSpeed;
    private WallJumper _jumper;

    public CharacterStateWallSlide(CharacterStateMachine stateMachine, CharacterState characterState, Rigidbody2D rb, InputHandler input, float slideSpeed)
        : base(stateMachine, characterState, rb, input)
    {
        _slideSpeed = slideSpeed;
    }

    public override void Enter()
    {
        Debug.Log("Enter State Wall Slide");
        if (_jumper == null)
        {
            _jumper = _rb.GetComponent<WallJumper>();
        }
    }

    public override void Update()
    {
        _rb.linearVelocity = new Vector2(_rb.linearVelocity.x, Mathf.Max(_rb.linearVelocity.y, -_slideSpeed));

        if (!_characterState.IsOnWall)
        {
            _stateMachine.ChangeState(_stateMachine.AirborneState);
        }

        if (_input.IsJumpPressed)
        {
            _jumper.WallJump(_rb);
            _characterState.FlipDirection();
            _stateMachine.ChangeState(_stateMachine.JumpingState);
        }

        if (_characterState.IsGrounded)
        {
            _stateMachine.ChangeState(_stateMachine.GroundedState);
        }
    }

}
