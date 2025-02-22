using UnityEngine;

public abstract class CharacterStateBase
{
    protected CharacterStateMachine _stateMachine;
    protected CharacterState _characterState;
    protected Rigidbody2D _rb;
    protected InputHandler _input;

    public CharacterStateBase(CharacterStateMachine stateMachine, CharacterState characterState, Rigidbody2D rb, InputHandler input)
    {
        _stateMachine = stateMachine;
        _characterState = characterState;
        _rb = rb;
        _input = input;
    }

    public virtual void Enter() { }
    public virtual void Exit() { }
    public virtual void HandleInput(Vector2 input) { }
    public virtual void Update() { }
    public virtual void FixedUpdate() { }
}
