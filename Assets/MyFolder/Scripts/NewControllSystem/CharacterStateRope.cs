using UnityEngine;

public class CharacterStateRope : CharacterStateBase
{
    private float _climbSpeed;
    private float _swingForce;
    private HingeJoint2D _hinge;
    private Rigidbody2D _rope;
    private RopeDetector _ropeDetector;

    public CharacterStateRope(CharacterStateMachine stateMachine, CharacterState characterState, Rigidbody2D rb, InputHandler input, float climbSpeed)
        : base(stateMachine, characterState, rb, input)
    {
        _climbSpeed = climbSpeed;
        _swingForce = 2f; // ћожно вынести в параметры конструктора
        _hinge = _rb.GetComponent<HingeJoint2D>();
        _hinge.enabled = false;
    }

    public void AttachToRope(Rigidbody2D rope, RopeDetector ropeDetector)
    {
        _rope = rope;
        _hinge.enabled = true;
        _hinge.connectedBody = _rope;
        _hinge.autoConfigureConnectedAnchor = true;
        _rb.linearVelocity = Vector2.zero;
        _ropeDetector = ropeDetector;
    }

    public override void HandleInput(Vector2 input)
    {
        if (input.x < 0)
            _rb.AddForce(Vector2.left * _swingForce, ForceMode2D.Force);

        if (input.x > 0)
            _rb.AddForce(Vector2.right * _swingForce, ForceMode2D.Force);

        if (input.y > 0)
            ClimbRope(1);

        if (input.y < 0)
            ClimbRope(-1);

        if (_input.IsJumpPressed)
        {
            _stateMachine.ChangeState(_stateMachine.JumpingState);
        }
        if ((input.x > 0 && !_characterState.IsFacingRight) || (input.x < 0 && _characterState.IsFacingRight))
        {
            _characterState.FlipDirection();
        }
    }

    public override void Update()
    {
        if (_rope == null) // ≈сли по какой-то причине веревка пропала
        {
            _stateMachine.ChangeState(_stateMachine.AirborneState);
        }
    }

    public override void Exit()
    {
        _hinge.enabled = false;
        _hinge.connectedBody = null;
        _rope = null;
        _characterState.SetOnRope(false);
        _hinge.autoConfigureConnectedAnchor = true;
        _ropeDetector.PauseDetecting();
    }

    private void ClimbRope(float direction)
    {
        _hinge.autoConfigureConnectedAnchor = false;
        Vector2 anchor = _hinge.connectedAnchor;
        float ropeLength = _hinge.connectedBody.transform.localScale.y;
        float movement = direction * _climbSpeed * Time.deltaTime / ropeLength;

        anchor.y += movement;
        anchor.y = Mathf.Clamp(anchor.y, -0.5f, 0.5f);
        _hinge.connectedAnchor = anchor;
    }
}
