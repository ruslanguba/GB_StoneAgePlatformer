using System;
using UnityEngine;

public class FruitCounter : MonoBehaviour
{
    public event Action<int> OnFruitValueChanged;
    [SerializeField] private FruitCollector _fruitCollector;
    private int _fruitsCollected = 0;

    private void Awake()
    {
        _fruitCollector = FindFirstObjectByType<FruitCollector>();
    }

    private void OnEnable()
    {
        _fruitCollector.OnFruitFound += FruitCollected;
    }

    private void OnDisable()
    {
        _fruitCollector.OnFruitFound -= FruitCollected;
    }

    private void FruitCollected()
    {
        OnFruitValueChanged?.Invoke(_fruitsCollected);
        _fruitsCollected++;
    }
}
