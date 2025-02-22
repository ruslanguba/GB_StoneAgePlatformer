using UnityEngine;

public class CharacterWallSlide : MonoBehaviour
{
    [SerializeField] private float _wallSlideSpeed = 2f;
    private Rigidbody2D _rb;
    private CharacterState _characterState;

    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _characterState = GetComponent<CharacterState>();
    }

    void Update()
    {
        if (_characterState.IsOnWall && !_characterState.IsGrounded)
        {
            ApplyWallSlide();
        }
    }

    private void ApplyWallSlide()
    {
        if (_rb.linearVelocity.y < 0)
        {
            _rb.linearVelocity = new Vector2(_rb.linearVelocity.x, -_wallSlideSpeed);
        }
    }
}
