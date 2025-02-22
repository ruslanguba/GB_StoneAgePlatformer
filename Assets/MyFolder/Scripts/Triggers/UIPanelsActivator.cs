using UnityEngine;

public class UIPanelsActivator : MonoBehaviour
{
    [SerializeField] private GameObject _advicePanel;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.GetComponent<Character>() != null)
        {
            _advicePanel.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        _advicePanel?.SetActive(false);
    }
}
