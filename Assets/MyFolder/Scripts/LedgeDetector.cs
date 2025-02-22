using System.Collections;
using UnityEngine;

public class LedgeDetector : MonoBehaviour
{
    [SerializeField] private Transform _ledgeCheckPivot;
    [SerializeField] private Transform _wallCheckPivot;
    [SerializeField] private float _ledgeCheckDistanse = 0.2f;
    [SerializeField] private float _stopCheckingTime = 0.2f;
    [SerializeField] private LayerMask _jumpableLayer;
    private CharacterState _characterState;
    private CharacterStateMachine _characterStateMachine;
    [SerializeField] private bool _isOnLedge;
    [SerializeField] private bool _isCheckingPaused;

    private void Start()
    {
        _characterState = GetComponentInParent<CharacterState>();
        _characterStateMachine = GetComponent<CharacterStateMachine>();
    }

    private void FixedUpdate()
    {
        if (!_isCheckingPaused && !_isOnLedge)
        {
            CheckLedge();
        }
    }

    private void CheckLedge()
    {
        if (_characterState.IsOnWall)
        {
            Vector2 checkDirection = Vector2.zero;
            if (_characterState.IsFacingRight)
            {
                checkDirection = Vector2.right;
            }
            else
            {
                checkDirection = Vector2.left;
            }
            RaycastHit2D upperHit = Physics2D.Raycast(_ledgeCheckPivot.position, checkDirection, _ledgeCheckDistanse, _jumpableLayer);
            RaycastHit2D lowerHit = Physics2D.Raycast(_wallCheckPivot.position, checkDirection, _ledgeCheckDistanse, _jumpableLayer);
            if (upperHit.collider == null && lowerHit.collider != null)
            {
                _isOnLedge = true;
            }
            else
            {
                _isOnLedge = false;
            }
            _characterState.SetOnLedge(_isOnLedge);
        }
    }

    public void PauseChecking()
    {
        _isOnLedge = false;
        _isCheckingPaused = true;
        StartCoroutine(ContinueCheckingLedge());
    }

    IEnumerator ContinueCheckingLedge()
    {
        yield return new WaitForSeconds(_stopCheckingTime);
        _isCheckingPaused = false;
    }
}
