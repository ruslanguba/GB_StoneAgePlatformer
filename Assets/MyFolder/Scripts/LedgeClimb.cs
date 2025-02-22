using UnityEngine;

public class LedgeClimb : MonoBehaviour
{
    [SerializeField] private Transform _targetOnLedge;
    //[SerializeField] private Transform _distanseCheckPivot;
    //[SerializeField] private float _speed = 2f;
    //[SerializeField] private float _stoppingDistance = 0.5f;
    //[SerializeField] private float _hightOffset = 1;
    [SerializeField] private LayerMask _wallLayer;

    //[SerializeField] private bool _isMovingToTarget = false;
    //[SerializeField] private bool _isMovingUp = false;

    //[SerializeField] private Vector2 _targetOnLedgeOriginalPosition;
    //[SerializeField] private Vector2 _targetToMove;
    //[SerializeField] private float _originalGravity;
    //[SerializeField] private LedgeDetector _detector;
    [SerializeField] private Rigidbody2D _rb;

    private void Start()
    {
        //_targetOnLedgeOriginalPosition = _targetOnLedge.localPosition;
        //_detector = GetComponentInChildren<LedgeDetector>();
        _rb = GetComponent<Rigidbody2D>();
    }

    //private void FixedUpdate()
    //{ 
    //    if (_isMovingToTarget)
    //    {
    //        MoveToTarget();
    //    }
    //}

    //public void StartPullUp()
    //{
    //    _targetOnLedge.transform.parent = null;
    //    _targetToMove = _targetOnLedge.transform.position;
    //    _isMovingToTarget = true;
    //    _isMovingUp = true;
    //}

    //private void MoveToTarget()
    //{
    //    if (_isMovingToTarget)
    //    {
    //        if (_isMovingUp)
    //        {
    //            Vector2 direction = new Vector2(transform.position.x, _targetToMove.y + _hightOffset);
    //            transform.position = Vector2.MoveTowards(transform.position, direction, Time.fixedDeltaTime * _speed);
    //            if (Mathf.Abs(transform.position.y - direction.y) < _stoppingDistance)
    //            {
    //                _isMovingUp = false;
    //            }
    //        }
    //        else
    //        {
    //            transform.position = Vector2.MoveTowards(transform.position, _targetToMove, Time.fixedDeltaTime * _speed);
    //            if (Vector2.Distance(_distanseCheckPivot.position, _targetToMove) < _stoppingDistance)
    //            {
    //                DropFromLedge();
    //            }
    //        }
    //    }
    //}

    //public void DropFromLedge()
    //{
    //    Debug.Log("Drop From Ledge");
    //    _targetOnLedge.transform.parent = transform;
    //    _targetOnLedge.localPosition = _targetOnLedgeOriginalPosition;
    //    _rb.gravityScale = _originalGravity;
    //    _isMovingToTarget = false;
    //    _detector.PauseChecking();
    //}

    //public void OnLedgeGrab()
    //{ 
    //    _rb.linearVelocity = Vector2.zero;
    //    _originalGravity = _rb.gravityScale;
    //    _rb.gravityScale = 0;
    //}

    public bool ClimbToWall(float direction)
    {
        Vector2 checkPosition = new Vector2(_rb.position.x + direction * 0.5f, _rb.position.y - 0.2f);
        bool isWall = Physics2D.OverlapCircle(checkPosition, 0.2f, _wallLayer);
        return isWall;
    }
}
