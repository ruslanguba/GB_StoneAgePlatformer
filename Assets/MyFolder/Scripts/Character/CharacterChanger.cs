using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class CharacterChanger : MonoBehaviour
{
    [SerializeField] private Character _mainCharacter;
    [SerializeField] private Character _petCharacter;
    [SerializeField] private Character _currentActiveCharacter;
    [SerializeField] private CameraFollower _follower;
    [SerializeField] private PetHandler _petHandler;
    public Character GetCurrentCharacter => _currentActiveCharacter;
    private bool _mainActive = true;
    private bool _isPetUsed = false;

    private void Start()
    {
        _petHandler = GetComponent<PetHandler>();
        _petCharacter.StopCharacter();
        if (_currentActiveCharacter == null)
        {
            _currentActiveCharacter = _mainCharacter;
        }
    }
    public void ChangeCharacter()
    {
        if(!_isPetUsed) 
            return;
        if (_mainActive)
        {
            _mainCharacter.StopCharacter();
            _petCharacter.ActivateCharacter();
            _currentActiveCharacter = _petCharacter;
            _mainActive = false;
        }
        else
        {
            _mainCharacter.ActivateCharacter();
            _petCharacter.StopCharacter();
            _currentActiveCharacter = _mainCharacter;
        }
        SetCameraTarget();
    }

    public void SetCameraTarget()
    {
        _follower.SetTarget(_currentActiveCharacter.transform);
    }

    public void UsePet()
    {
        if(_isPetUsed)
        {
            _petHandler.ReturnPet();
            ChangeCharacter();
            _isPetUsed = false;
        }
        else
        {
            _petHandler.UsePet();
            _isPetUsed = true;
        }
    }

    public void ReternPet()
    {
        _petHandler.ReturnPet();
    }
}
