using UnityEngine;

public class CharacterRope : MonoBehaviour, IMovable
{
    [SerializeField] private float _swingForce = 5;
    [SerializeField] private float _climbSpeed = 2;
    private CharacterState _characterState;
    private RopeDetector _ropeDetector;
    private HingeJoint2D _hinge;
    private Rigidbody2D _rb;

    void Start()
    {
        _characterState = GetComponent<CharacterState>();
        _ropeDetector = GetComponentInChildren<RopeDetector>();
        _rb = GetComponent<Rigidbody2D>();
        _hinge = GetComponent<HingeJoint2D>();
        _hinge.enabled = false;
    }

    public void AttachToRope(Rigidbody2D rope)
    {
        _hinge.enabled = true;
        _hinge.connectedBody = rope;
        _characterState.SetOnRope(true);
        _rb.linearVelocity = Vector2.zero;
        Debug.Log("Rope");
    }

    public void ReleaseRope()
    {
        if (_characterState.IsOnRope)
        {
            _hinge.enabled = false;
            _hinge.connectedBody = null;
            _characterState.SetOnRope(false);
            _hinge.autoConfigureConnectedAnchor = true;
            _ropeDetector.PauseDetecting();
        }
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

    public void HandleMove(Vector2 direction)
    {

        if (_characterState.IsOnRope)
        {
            if (direction.x < 0)
                _rb.AddForce(Vector2.left * _swingForce, ForceMode2D.Force);

            if (direction.x > 0)
                _rb.AddForce(Vector2.right * _swingForce, ForceMode2D.Force);

            if (direction.y > 0)
                ClimbRope(1);

            if (direction.y < 0)
                ClimbRope(-1);
        }
    }
}
