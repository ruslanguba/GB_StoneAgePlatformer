using UnityEngine;

public class LedgeGrab : MonoBehaviour, IMovable
{
    [SerializeField] private Transform _targetOnLedge;
    [SerializeField] private Transform _distanseCheckPivot;
    [SerializeField] private float _speed = 2f;
    [SerializeField] private float _stoppingDistance = 0.5f;
    [SerializeField] private float _hightOffset = 1;
    [SerializeField] private LayerMask _wallLayer;

    [SerializeField] private bool _isMovingToTarget = false;
    [SerializeField] private bool _isMovingUp = false;
    [SerializeField] private bool _isGrabed = false;

    [SerializeField] private Vector2 _targetOnLedgeOriginalPosition;
    [SerializeField] private float _originalGravity;
    [SerializeField] private CharacterState _characterState;
    [SerializeField] private LedgeDetector _detector;
    [SerializeField] private Rigidbody2D _rb;

    private void Start()
    {
        _targetOnLedgeOriginalPosition = _targetOnLedge.localPosition;
        _characterState = GetComponent<CharacterState>();
        _detector = GetComponentInChildren<LedgeDetector>();
        _rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        if(_characterState.IsOnLedge && !_isGrabed && !_isMovingToTarget)
        {
            _isGrabed = true;
            OnLedgeGrab();
        }

        if (_characterState.IsOnLedge && _isMovingToTarget)
        {
            _isGrabed = false;
            MoveToTarget();
        }
    }

    //public void HandleLedgeMovement(Vector2 direction)
    //{
    //    if(_characterState.IsOnLedge)
    //    {
    //        if (direction.y > 0)
    //        {
    //            StartPullUp();
    //        }

    //        if (direction.y < 0)
    //        {
    //            DropFromLedge();
    //        }

    //        if (direction.x > 0)
    //        {
    //            bool right = Physics2D.OverlapCircle(new Vector2(transform.position.x + 0.5f, transform.position.y - 0.2f), 0.2f, _wallLayer);
    //            if (right == true)
    //            {
    //                StartPullUp();
    //            }
    //            else
    //            {
    //                DropFromLedge();
    //            }
    //        }
    //        if (direction.x < 0)
    //        {
    //            bool left = Physics2D.OverlapCircle(new Vector2(transform.position.x - 0.5f, transform.position.y - 0.2f), 0.2f, _wallLayer);
    //            if (left == true)
    //            {
    //                StartPullUp();
    //            }
    //            else
    //            {
    //                DropFromLedge();
    //            }
    //        }
    //    }      
    //}

    public void StartPullUp()
    {
        if (!_characterState.IsOnLedge) 
            return;

        if (_characterState.IsOnLedge && !_isMovingToTarget)
        {
            _targetOnLedge.transform.parent = null;
            _isMovingToTarget = true;
            _isMovingUp = true;
        }
    }

    private void MoveToTarget()
    {
        if (!_isMovingToTarget) return;

        Vector2 targetPosition = _targetOnLedge.position;
        if (_isMovingUp)
        {
            // Поднимаемся вверх
            Vector2 direction = new Vector2(transform.position.x, targetPosition.y + _hightOffset);
            transform.position = Vector2.MoveTowards(transform.position, direction, Time.fixedDeltaTime * _speed);

            // Если достиг нужной высоты, переходим к движению вбок
            if (Mathf.Abs(transform.position.y - direction.y) < _stoppingDistance)
            {
                _isMovingUp = false;
            }
        }
        else
        {
            transform.position = Vector2.MoveTowards(transform.position, targetPosition, Time.fixedDeltaTime * _speed);
            if (Vector2.Distance(_distanseCheckPivot.position, targetPosition) < _stoppingDistance)
            {
                DropFromLedge();
            }
        }
    }

    public void DropFromLedge()
    {
        _targetOnLedge.transform.parent = transform;
        _targetOnLedge.localPosition = _targetOnLedgeOriginalPosition;
        _rb.gravityScale = _originalGravity;
        _isMovingToTarget = false;
        _isGrabed = false;
        _characterState.SetOnLedge(false);
        _detector.PauseChecking();
    }

    public void OnLedgeGrab()
    {
        _characterState.SetOnLedge(true);
        _rb.linearVelocity = Vector2.zero;
        _originalGravity = _rb.gravityScale;
        _rb.gravityScale = 0;
    }

    public void HandleMove(Vector2 direction)
    {
        if (_characterState.IsOnLedge)
        {
            if (direction.y > 0)
            {
                StartPullUp();
            }

            if (direction.y < 0)
            {
                DropFromLedge();
            }

            if (direction.x > 0)
            {
                bool right = Physics2D.OverlapCircle(new Vector2(transform.position.x + 0.5f, transform.position.y - 0.2f), 0.2f, _wallLayer);
                if (right == true)
                {
                    StartPullUp();
                }
                else
                {
                    DropFromLedge();
                }
            }
            if (direction.x < 0)
            {
                bool left = Physics2D.OverlapCircle(new Vector2(transform.position.x - 0.5f, transform.position.y - 0.2f), 0.2f, _wallLayer);
                if (left == true)
                {
                    StartPullUp();
                }
                else
                {
                    DropFromLedge();
                }
            }
        }
    }
}
