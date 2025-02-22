using UnityEngine;

public class CharacterStateMachine : MonoBehaviour
{
    private CharacterStateBase _currentState;
    public CharacterStateGrounded GroundedState { get; private set; }
    public CharacterStateJumping JumpingState { get; private set; }
    public CharacterStateAirborne AirborneState { get; private set; }
    public CharacterStateWallSlide WallSlideState { get; private set; }
    public CharacterStateLedgeHang LedgeHangState { get; private set; }
    public CharacterStateRope RopeState { get; private set; }
    public CharacterStateLedgeClimbing LedgeClimbState { get; private set; }

    [SerializeField] private CharacterState _characterState;
    [SerializeField] private Rigidbody2D _rb;
    [SerializeField] private InputHandler _input;

    private void Awake()
    {
        _characterState = GetComponent<CharacterState>();
        _rb = GetComponent<Rigidbody2D>();
        _input = GetComponent<InputHandler>();

        GroundedState = new CharacterStateGrounded(this, _characterState, _rb, _input, 5f);
        JumpingState = new CharacterStateJumping(this, _characterState, _rb, _input, 5f);
        AirborneState = new CharacterStateAirborne(this, _characterState, _rb, _input, 5f);
        WallSlideState = new CharacterStateWallSlide(this, _characterState, _rb, _input, 1f);
        RopeState = new CharacterStateRope(this, _characterState, _rb, _input, 2f);
        LedgeHangState = new CharacterStateLedgeHang(this, _characterState, _rb, _input);
        LedgeClimbState = new CharacterStateLedgeClimbing(this, _characterState, _rb, _input, 2);

        _currentState = GroundedState;
    }

    private void Start()
    {
        ChangeState(GroundedState);
    }

    private void Update()
    {
        _currentState.Update();
    }

    private void FixedUpdate()
    {
        _currentState.FixedUpdate();
    }

    public void ChangeState(CharacterStateBase newState)
    {
        if (_currentState == newState) return;

        _currentState.Exit();
        _currentState = newState;
        _currentState.Enter();
    }

    public void HandleInput(Vector2 input)
    {
        _currentState.HandleInput(input);
    }
}
