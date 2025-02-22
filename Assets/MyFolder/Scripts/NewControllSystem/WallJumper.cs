using UnityEngine;

public class WallJumper : MonoBehaviour
{
    [SerializeField] private float _wallJumpForce = 5;
    [SerializeField] private float _wallCheckDistance = 0.7f;
    [SerializeField] private LayerMask _jumpableLayer;
    public void WallJump(Rigidbody2D rb)
    {
        Debug.Log("WallJump");
        RaycastHit2D hitLeft = Physics2D.Raycast(transform.position, Vector2.left, _wallCheckDistance, _jumpableLayer);
        RaycastHit2D hitRight = Physics2D.Raycast(transform.position, Vector2.right, _wallCheckDistance, _jumpableLayer);
        Vector2 jumpDirection = Vector2.zero;
        if (hitRight.collider != null)
        {
            jumpDirection = new Vector2(-_wallJumpForce, rb.linearVelocity.y);
        }
        else if (hitLeft.collider != null)
        {
            jumpDirection = new Vector2(_wallJumpForce, rb.linearVelocity.y);
        }
        rb.AddForce(jumpDirection, ForceMode2D.Impulse);
    }
}
