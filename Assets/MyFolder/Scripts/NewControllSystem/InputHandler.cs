using System;
using UnityEngine;

public class InputHandler : MonoBehaviour
{
    [SerializeField] private CharacterStateMachine _stateMachine;

    private void Update()
    {
        SetMoveDidection();
        if (Input.GetKeyDown(KeyCode.Space))
        {
            OnJump();
        }
    }

    private void SetMoveDidection()
    {
        float vertical = Input.GetAxis("Vertical");
        float horizontal = Input.GetAxis("Horizontal");
        Vector2 moveDirection = new Vector2(horizontal, vertical);
        _stateMachine.HandleInput(moveDirection);
    }

    private void Awake()
    {
        _stateMachine = GetComponent<CharacterStateMachine>();
    }

    public void OnMove(Vector2 input)
    {
        _stateMachine.HandleInput(input);
    }

    public bool IsJumpPressed => Input.GetKeyDown(KeyCode.Space);
    public void OnJump()
    {
        _stateMachine.ChangeState(_stateMachine.JumpingState);
    }
}
