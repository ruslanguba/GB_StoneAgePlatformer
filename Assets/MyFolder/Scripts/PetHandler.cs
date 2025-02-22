using System.Collections;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class PetHandler : MonoBehaviour
{
    [SerializeField] private Transform _petTransform;
    [SerializeField] private Transform _originalPetPosition;
    [SerializeField] private float _speed = 10;
    [SerializeField] private Rigidbody2D _rb;
    [SerializeField] private Collider2D _collider;
    [SerializeField] private RopeDetector _detector;
    private Collider2D _ropeDetector;
    private bool _isPetActive;

    private void Start()
    {
        _rb = _petTransform.GetComponent<Rigidbody2D>();
        _detector = _petTransform.GetComponentInChildren<RopeDetector>();
        _collider = _petTransform.GetComponentInChildren<Collider2D>();
        _ropeDetector = _detector.GetComponent<Collider2D>();
        _rb.bodyType = RigidbodyType2D.Kinematic;
        _collider.enabled = false;
        _detector.enabled = false;
        _ropeDetector.enabled = false;
        _petTransform.position = _originalPetPosition.position;
    }

    private void Update()
    {
        if (!_isPetActive)
        {
            _petTransform.position = _originalPetPosition.position;
        }
    }
    public void UsePet()
    {
        _rb.bodyType = RigidbodyType2D.Dynamic;
        _detector.enabled = true;
        _collider.enabled = true;
        _isPetActive = true;
        _ropeDetector.enabled = true;
    }

    public void ReturnPet()
    {
        _rb.bodyType = RigidbodyType2D.Kinematic;
        StartCoroutine(MoveOverTime());
    }

    private IEnumerator MoveOverTime()
    {
        _collider.enabled = false;
        _detector.enabled = false;
        while (Vector2.Distance(_petTransform.transform.position, _originalPetPosition.position) > 0.01f)
        {
            _petTransform.transform.position = Vector2.MoveTowards(_petTransform.position, _originalPetPosition.position, _speed * Time.deltaTime);
            yield return null;
        }

        _petTransform.transform.position = _originalPetPosition.position; // Устанавливаем точно в точку
        _petTransform.position = _originalPetPosition.position;
        _isPetActive = false;
        _ropeDetector.enabled = false;
    }
}
