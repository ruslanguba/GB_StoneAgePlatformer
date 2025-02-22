using UnityEngine;

public class CharacterStateAirborne : CharacterStateBase
{
    private float _airControlFactor = 0.8f; // Как сильно игрок может управлять движением в воздухе
    private float _fallMultiplier = 1f; // Усиленная гравитация при падении
    private float _movementSpeed; // Скорость движения персонажа

    public CharacterStateAirborne(CharacterStateMachine stateMachine, CharacterState characterState, Rigidbody2D rb, InputHandler input, float movementSpeed)
        : base(stateMachine, characterState, rb, input)
    {
        _movementSpeed = movementSpeed;
    }

    public override void Enter()
    {
        Debug.Log("Enter State Airborne");
        _rb.gravityScale = 1;
    }

    public override void HandleInput(Vector2 input)
    {
        if (input != Vector2.zero)
        {
            _rb.AddForce(new Vector2(input.x * _movementSpeed * _airControlFactor, 0), ForceMode2D.Force);
        }

        if (_rb.linearVelocity.y < 0)
        {
            _rb.gravityScale = _fallMultiplier;
        }

        if ((input.x > 0 && !_characterState.IsFacingRight) || (input.x < 0 && _characterState.IsFacingRight))
        {
            _characterState.FlipDirection();
        }
    }

    public override void Update()
    {
        // Если персонаж касается земли, переключаем в состояние "на земле"
        if (_characterState.IsGrounded)
        {
            _stateMachine.ChangeState(_stateMachine.GroundedState);
            return;
        }

        // Если персонаж зацепился за край, переключаем в висение
        if (_characterState.IsOnLedge)
        {
            Debug.Log("Ledge FOUND!!!!");
            _stateMachine.ChangeState(_stateMachine.LedgeHangState);
            return;
        }

        // Если персонаж касается стены, можно включить скольжение (если такая механика есть)
        if (_characterState.IsOnWall && _rb.linearVelocity.y < 0)
        {
            _stateMachine.ChangeState(_stateMachine.WallSlideState);
            return;
        }

        if (_characterState.IsOnRope) 
        {
            _stateMachine.ChangeState(_stateMachine.RopeState);
            return;
        }
    }

    public override void Exit()
    {
        _rb.gravityScale = 1f;
    }
}
