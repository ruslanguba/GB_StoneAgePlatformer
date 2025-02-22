using UnityEngine;

public class ActivatorTrigger : MonoBehaviour
{
    private Animator _animator;
    void Start()
    {
        _animator = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Character>() != null)
        {
            _animator.SetTrigger("Act");
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.GetComponent<Movable>() != null)
        {
            _animator.SetTrigger("Run");
        }
    }
}
