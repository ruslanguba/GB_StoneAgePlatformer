using System.Collections;
using UnityEngine;

public class RopeExtender : MonoBehaviour, IInteractable
{
    [SerializeField] private float _extendDuration = 2f;
    [SerializeField] private float _targetLength = 10f;

    private float _startLength;
    private Vector3 _initialPosition;
    private Vector3 _initialScale;
    private Rigidbody2D _rb;

    void Start()
    {
        _initialPosition = transform.position;
        _initialScale = transform.localScale;
        _startLength = _initialScale.y;
        _rb = GetComponent<Rigidbody2D>();
        _rb.bodyType = RigidbodyType2D.Static;
    }

    public void ExtendRope()
    {
        StartCoroutine(ExtendRoutine(_targetLength));
    }

    private IEnumerator ExtendRoutine(float newLength)
    {
        float elapsedTime = 0f;
        float initialLength = transform.localScale.y;
        Vector3 initialPos = transform.position;

        while (elapsedTime < _extendDuration)
        {
            elapsedTime += Time.deltaTime;
            float t = elapsedTime / _extendDuration;

            float currentLength = Mathf.Lerp(initialLength, newLength, t);
            transform.localScale = new Vector3(_initialScale.x, currentLength, _initialScale.z);

            float lengthChange = currentLength - initialLength;
            transform.position = new Vector2(initialPos.x, initialPos.y - lengthChange / 2f);

            yield return null;
        }
        _rb.bodyType = RigidbodyType2D.Dynamic;
        transform.localScale = new Vector2(_initialScale.x, newLength);
        transform.position = new Vector2(_initialPosition.x, _initialPosition.y - (newLength - _startLength) / 2f);
    }

    public void Interract()
    {
        ExtendRope();
    }
}
