using UnityEngine;

public class CharacterStateLedgeClimbing : CharacterStateBase
{
    private float _speed;
    private float _hightOffset = 0.5f;
    private float _stoppingDistance = 0.5f;
    Vector2 _targetPosition;
    private bool _isMovingUp;
    private bool _isMoving;

    public CharacterStateLedgeClimbing(CharacterStateMachine stateMachine, CharacterState characterState, Rigidbody2D rb, InputHandler input, float speed)
        : base(stateMachine, characterState, rb, input)
    {
        _speed = speed;
    }

    public override void Enter()
    {
        Debug.Log("Enter State Ledge Climbing");
        _rb.gravityScale = 0;
        _targetPosition = _rb.position + new Vector2(1, 1.2f);
    }

    public void StartClimbing(Vector2 targetPosition)
    {
        _targetPosition = targetPosition;
        _isMoving = true;
        _isMovingUp = true;
    }

    public override void Update()
    {
        if (_isMoving)
        {
            if (_isMovingUp)
            {
                Vector2 direction = new Vector2(_rb.transform.position.x, _targetPosition.y + _hightOffset);
                _rb.transform.position = Vector2.MoveTowards(_rb.transform.position, direction, Time.fixedDeltaTime * _speed);
                if (_rb.transform.position.y - direction.y > 1)
                {
                    _isMovingUp = false;
                }
            }
            else
            {
                _rb.transform.position = Vector2.MoveTowards(_rb.transform.position, new Vector2(_targetPosition.x, _targetPosition.y + _hightOffset), Time.fixedDeltaTime * _speed);
                if (Vector2.Distance(_rb.transform.position, _targetPosition) < _stoppingDistance)
                {
                    _stateMachine.ChangeState(_stateMachine.GroundedState);
                    _isMoving = false;
                }
            }
        }
    }

    public override void Exit()
    {
        _rb.gravityScale = 1;
    }
}
