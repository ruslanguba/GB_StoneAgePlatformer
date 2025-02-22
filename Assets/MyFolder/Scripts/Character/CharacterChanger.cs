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
    [SerializeField] private bool _mainActive = true;
    [SerializeField] private bool _isPetUsed = false;
    [SerializeField] private bool _isPetOnMainCharacter = true;

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
        if(_isPetOnMainCharacter) 
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
            _mainActive = true;
        }
        SetCameraTarget();
    }

    public void SetCameraTarget()
    {
        _follower.SetTarget(_currentActiveCharacter.transform);
    }

    public void CallPet()
    {
        if (!_isPetUsed)
        {
            UsePet();
        }
        else
        {
            ReturnPet();
        }

        //if (_isPetUsed)
        //{
        //    ReturnPet();
        //    ChangeCharacter();
        //    _isPetUsed = false;
        //}
        //else
        //{
        //    UsePet();
        //    _isPetOnMainCharacter = false;
        //    _isPetUsed = true;
        //}
    }

    public void UsePet()
    {
        _petHandler.UsePet();
        _isPetUsed = true;
        _isPetOnMainCharacter = false;
    }

    public void ReturnPet()
    {
        if (_currentActiveCharacter == _petCharacter)
        {
            ChangeCharacter();
        }
        _petHandler.ReturnPet();
        _isPetUsed = false;
        _isPetOnMainCharacter = true;
    }
}
