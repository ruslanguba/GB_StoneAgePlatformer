using UnityEngine;

public class Fruit : MonoBehaviour
{
    public void Collected()
    {
        GetComponent<Collider2D>().enabled = false;
        Destroy(gameObject);
    }
}
