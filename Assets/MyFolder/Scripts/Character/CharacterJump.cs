using System.Collections;
using UnityEngine;

public class CharacterJump : MonoBehaviour
{
    [SerializeField] private float _jumpForce = 4;
    [SerializeField] private float _doubleJumpForce = 3;
    [SerializeField] private float _wallJumpForce = 4;
    [SerializeField] private float _wallCheckDistance = 0.7f;
    [SerializeField] private float _horizontalVelocityReduction = 2;
    [SerializeField] private LayerMask _jumpableLayer;
    private int _jumpsCount;
    private Rigidbody2D _rb;
    private CharacterState _characterState;
    private CharacterMovement _characterMovement;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _characterState = GetComponent<CharacterState>();
        _characterMovement = GetComponent<CharacterMovement>();
    }

    public void Jump()
    {
        if (_characterState.IsOnLedge)
        {
            return;
        }

        else if(_characterState.IsOnWall && !_characterState.IsGrounded)
        {
            WallJump();
        }

        else if (_characterState.IsGrounded)
        {
            _jumpsCount = 0;
            _rb.AddForce(Vector2.up * _jumpForce, ForceMode2D.Impulse);
            //_rb.AddForce(new Vector2(_rb.linearVelocity.x / _horizontalVelocityReduction, _jumpForce), ForceMode2D.Impulse);
            _jumpsCount++;
        }

        else if (!_characterState.IsGrounded && _jumpsCount < 2)
        {
            _jumpsCount++;
            _rb.AddForce(Vector2.up * _doubleJumpForce, ForceMode2D.Impulse);
        }

        else if(_characterState.IsOnRope)
        {
            _rb.AddForce(Vector2.up * _jumpForce, ForceMode2D.Impulse);
        }
    }

    private void WallJump()
    {
        _characterState.SetOnLedge(false);
        RaycastHit2D hitLeft = Physics2D.Raycast(transform.position, Vector2.left, _wallCheckDistance, _jumpableLayer);
        RaycastHit2D hitRight = Physics2D.Raycast(transform.position, Vector2.right, _wallCheckDistance, _jumpableLayer);
        Vector2 jumpDirection = Vector2.zero;
        if (hitRight.collider != null)
        {
            jumpDirection = new Vector2(-_wallJumpForce, _jumpForce + _doubleJumpForce);
        }
        else if (hitLeft.collider != null)
        {
            jumpDirection = new Vector2(_wallJumpForce, _jumpForce + _doubleJumpForce);
        }
        _rb.AddForce(jumpDirection, ForceMode2D.Impulse);
        _characterState.FlipDirection();
    }
}
