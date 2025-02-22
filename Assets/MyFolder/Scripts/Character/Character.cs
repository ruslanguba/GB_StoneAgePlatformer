using System;
using UnityEngine;

public class Character : MonoBehaviour
{
    public Action OnJump;
    public Action OnInteract;
    private Rigidbody2D _rb;
    private CharacterMovement _characterMovement;
    private CharacterJump _characterJump;
    private CharacterRope _characterRope;
    private CharacterWallSlide _characterWallSlide;
    private LedgeGrab _characterLedgeGrab;
    private CharacterInteractor _characterInteractor;
    private Vector2 _moveInput;

    void Start()
    {
        _characterJump = GetComponent<CharacterJump>();
        _characterRope = GetComponent<CharacterRope>();
        _characterLedgeGrab = GetComponent<LedgeGrab>();
        _characterWallSlide = GetComponent<CharacterWallSlide>();
        _characterMovement = GetComponent<CharacterMovement>();
        _characterInteractor = GetComponent<CharacterInteractor>();
        _rb = GetComponent<Rigidbody2D>();
    }

    public void ActivateCharacter()
    {
        _characterJump.enabled = true;
        _characterRope.enabled = true;
        _characterLedgeGrab.enabled = true;
        _characterWallSlide.enabled = true;
        _characterMovement.enabled = true;
        _characterInteractor.enabled = true;
    }

    public void StopCharacter()
    {
        _characterJump.enabled = false;
        _characterRope.enabled = false;
        _characterLedgeGrab.enabled = false;
        _characterWallSlide.enabled = false;
        _characterMovement.enabled = false;
        _characterInteractor.enabled = false;
        _rb.linearVelocity = Vector2.zero;
    }

    public void Interract()
    {
        OnInteract?.Invoke();
        _characterInteractor.Interact();
        Debug.Log("current character interacted");
    }

    public void Jump()
    {
        OnJump?.Invoke();   
        _characterJump.Jump();
        _characterRope.ReleaseRope();
        _characterLedgeGrab.StartPullUp();
    }

    private void FixedUpdate()
    {
        _characterMovement.HandleMove(_moveInput);
        _characterRope.HandleMove(_moveInput);
        _characterLedgeGrab.HandleMove(_moveInput);
    }

    public void MoveCharacter(Vector2 direction)
    {
        _moveInput = direction;
    }
}
