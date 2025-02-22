using UnityEngine;
using UnityEngine.UI;

public class UIFruitScore : MonoBehaviour
{
    [SerializeField] private Image[] _fruitsImagies;
    [SerializeField] private FruitCounter _fruitCounter;
    [SerializeField] private int _fruitIndex = 0;

    private void Awake()
    {
        _fruitCounter = FindFirstObjectByType<FruitCounter>();
    }

    private void OnEnable()
    {
        _fruitCounter.OnFruitValueChanged += ShowCollecteFruit;
    }

    private void OnDisable()
    {
        _fruitCounter.OnFruitValueChanged -= ShowCollecteFruit;
    }

    private void ShowCollecteFruit(int fruitsCollected)
    {
        if(fruitsCollected < _fruitsImagies.Length)
        {
            Color color = _fruitsImagies[fruitsCollected].color;
            color.a = 1;
            _fruitsImagies[fruitsCollected].color = color;
            _fruitIndex++;
        }
    }
}
