using System;
using System.Collections;
using UnityEngine;

public class RopeDetector : MonoBehaviour
{
    public event Action<Rigidbody2D> RopeFound;

    [SerializeField] private float _stopDetectingTimer = 0.1f;
    private CharacterRope _characterRope => GetComponentInParent<CharacterRope>();
    private CharacterState _characterState => GetComponentInParent<CharacterState>();
    //private CharacterStateMachine _characterStateMachine => GetComponentInParent<CharacterStateMachine>();
    private Collider2D _collider => GetComponent<Collider2D>();

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Rope>() != null)
        {
            _characterState.SetOnRope(true);
            //_characterStateMachine.ChangeState(_characterStateMachine.RopeState);
            //_characterStateMachine.RopeState.AttachToRope(collision.GetComponent<Rigidbody2D>(), this);
            _characterRope.AttachToRope(collision.GetComponent<Rigidbody2D>());
        }
    }

    public void PauseDetecting()
    {
        _collider.enabled = false;
        StartCoroutine(ActivateColliderWithDelay());
    }

    private IEnumerator ActivateColliderWithDelay()
    {
        yield return new WaitForSeconds(_stopDetectingTimer);
        _collider.enabled = true;
    }
}
