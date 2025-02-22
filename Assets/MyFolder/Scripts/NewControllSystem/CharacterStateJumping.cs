using UnityEngine;

public class CharacterStateJumping : CharacterStateBase
{
    private float _jumpForce;
    private int _jumpsLeft;
    private bool _canDoubleJump = true;

    public CharacterStateJumping(CharacterStateMachine stateMachine, 
            CharacterState characterState, 
            Rigidbody2D rb, 
            InputHandler input, 
            float jumpForce)
            : base(stateMachine, characterState, rb, input)
    {
        _jumpForce = jumpForce;
    }

    public override void Enter()
    {
        _jumpsLeft = _canDoubleJump ? 2 : 1;
        Jump();
    }

    public override void HandleInput(Vector2 input)
    {
        if (_input.IsJumpPressed && _jumpsLeft > 0)
        {
            Jump();
        }
    }

    private void Jump()
    {
        Debug.Log("JUMP");
        _rb.AddForce(Vector2.up * _jumpForce, ForceMode2D.Impulse);
        _jumpsLeft--;
        Debug.Log(_jumpsLeft);
    }

    public override void Update()
    {
        //if (_characterState.IsOnWall)
        //{
        //    _stateMachine.ChangeState(_stateMachine.WallSlideState);
        //}
        //if (_characterState.IsOnLedge)
        //{
        //    _stateMachine.ChangeState(_stateMachine.LedgeHangState);
        //}
        //if(_characterState.IsOnRope)
        //{
        //    _stateMachine.ChangeState(_stateMachine.RopeState);
        //}
        if (_rb.linearVelocity.y <= 0)
        {
            _stateMachine.ChangeState(_stateMachine.AirborneState);
        }
    }
}
