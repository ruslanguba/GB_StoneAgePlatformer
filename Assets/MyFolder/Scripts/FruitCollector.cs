using System;
using UnityEngine;

public class FruitCollector : MonoBehaviour
{
    public event Action OnFruitFound;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.GetComponent<Fruit>())
        {
            Fruit fruit = collision.GetComponent<Fruit>();
            FruitFound(fruit);
        }
    }

    void FruitFound(Fruit fruit)
    {
        OnFruitFound?.Invoke();
        fruit.Collected();
        Debug.Log("Fruit Collision Detected");
    }
}
