using UnityEngine;

public abstract class Follower : MonoBehaviour
{
    [SerializeField] private Transform _target;
    [SerializeField] private Vector3 _offset;
    [SerializeField] private float _smoothing;

    protected void Move()
    {
        var nextPosition = Vector3.Lerp(transform.position, _target.position + _offset, _smoothing * Time.fixedDeltaTime);
        transform.position = nextPosition;
    }

    public void SetTarget(Transform target)
    {
        _target = target;
    }
}
