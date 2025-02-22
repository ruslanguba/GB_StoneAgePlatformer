using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputHandler : MonoBehaviour
{
    private CharacterChanger _characterChanger;
    private PlayerInput _playerInput;
    //private CharacterMovement _characterMovement;
    //private CharacterJump _characterJump;
    //private CharacterRope _characterRope;
    //private LedgeGrab _ledgrGrab;
    private Vector2 _moveInput;

    private void Awake()
    {
        _playerInput = new PlayerInput();
        _playerInput.Enable();

    }
    void Start()
    {
        //_characterMovement = GetComponent<CharacterMovement>();
        //_characterRope = GetComponent<CharacterRope>();
        //_characterJump = GetComponent<CharacterJump>();
        //_ledgrGrab = GetComponent<LedgeGrab>();
        _characterChanger = GetComponent<CharacterChanger>();
    }

    private void OnEnable()
    {
        _playerInput.Gameplay.Jump.performed += JumpPerformed;
        _playerInput.Gameplay.Interact.performed += InteractPerformed;
        _playerInput.Gameplay.UsePet.performed += UsePetPerformed;
        _playerInput.Gameplay.ChangeCharacter.performed += ChangeCharacterPerformed;
    }

    private void ChangeCharacterPerformed(InputAction.CallbackContext obj)
    {
        _characterChanger.ChangeCharacter();
    }

    private void UsePetPerformed(InputAction.CallbackContext obj)
    {
        _characterChanger.CallPet();
    }

    private void OnDisable()
    {
        _playerInput.Gameplay.Jump.performed -= JumpPerformed;
        _playerInput.Gameplay.Interact.performed -= InteractPerformed;
        _playerInput.Gameplay.UsePet.performed -= UsePetPerformed;
        _playerInput.Gameplay.ChangeCharacter.performed -= ChangeCharacterPerformed;
    }

    private void InteractPerformed(InputAction.CallbackContext obj)
    {
        _characterChanger.GetCurrentCharacter.Interract();
    }

    private void JumpPerformed(InputAction.CallbackContext obj)
    {
        _characterChanger.GetCurrentCharacter.Jump();
        //_characterJump.Jump();
        //_characterRope.ReleaseRope();
        //_ledgrGrab.StartPullUp();
    }

    private void Update()
    {
        _moveInput = _playerInput.Gameplay.Movement.ReadValue<Vector2>();
        _characterChanger.GetCurrentCharacter.MoveCharacter(_moveInput);
        //_characterMovement.HandleMove(_moveInput);
        //_characterRope.HandleMove(_moveInput);
        //_ledgrGrab.HandleMove(_moveInput);
    }
}
