using UnityEngine;

public class CharacterState : MonoBehaviour
{
    [SerializeField] private Transform _groundCheckPivot;
    [SerializeField] private float _groundCheckRadius = 0.4f;
    [SerializeField] private Transform _wallCheckPivot;
    [SerializeField] private float _wallCheckRadius = 0.2f;
    [SerializeField] private LayerMask _jumpableLayer;

    [SerializeField] private bool _isGrounded;
    [SerializeField] private bool _isOnWall;
    [SerializeField] private bool _isOnRope;
    [SerializeField] private bool _isOnLedge;
    [SerializeField] private bool _isFacingRight = true;

    public bool IsFacingRight => _isFacingRight;
    public bool IsGrounded => _isGrounded;
    public bool IsOnWall => _isOnWall;
    public bool IsOnRope => _isOnRope;
    public bool IsOnLedge => _isOnLedge;

    private void FixedUpdate()
    {
        _isGrounded = Physics2D.OverlapCircle(_groundCheckPivot.position, _groundCheckRadius, _jumpableLayer);
        _isOnWall = Physics2D.OverlapCircle(_wallCheckPivot.position, _wallCheckRadius, _jumpableLayer);
    }

    public void SetOnRope(bool value) => _isOnRope = value;
    public void SetOnLedge(bool value) => _isOnLedge = value;

    public void FlipDirection()
    {
        _isFacingRight = !_isFacingRight;
        Vector3 localScale = transform.localScale;
        localScale.x *= -1;
        transform.localScale = localScale;
    }
}
