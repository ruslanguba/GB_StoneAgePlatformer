using UnityEngine;

public class CharacterStateLedgeHang : CharacterStateBase
{
    private LedgeClimb _ledgeClimb;

    public CharacterStateLedgeHang(CharacterStateMachine stateMachine, CharacterState characterState, Rigidbody2D rb, InputHandler input)
        : base(stateMachine, characterState, rb, input)
    {

    }

    public override void Enter()
    {
        Debug.Log("Entered Ledge Hang State");
        if (_ledgeClimb == null)
        {
            _ledgeClimb = _rb.GetComponent<LedgeClimb>();
        }
        _rb.linearVelocity = Vector2.zero;
        _rb.gravityScale = 0;
    }

    public override void HandleInput(Vector2 input)
    {
        if (input.y > 0) 
        {
            _stateMachine.ChangeState(_stateMachine.LedgeClimbState);
        }
        if (input.y < 0) 
        {
            _stateMachine.ChangeState(_stateMachine.AirborneState);
        }
        if (input.x != 0)
        {
            if(_ledgeClimb.ClimbToWall(input.x))
            {
                _stateMachine.ChangeState(_stateMachine.LedgeClimbState);
            }
            else
            {
                _stateMachine.ChangeState(_stateMachine.AirborneState);
            }
        }
        if (_input.IsJumpPressed)
        {
            _stateMachine.ChangeState(_stateMachine.LedgeClimbState);
        }
    }
    
    public override void Exit()
    {
        _rb.gravityScale = 0;
        _characterState.SetOnLedge(false);
    }
}
