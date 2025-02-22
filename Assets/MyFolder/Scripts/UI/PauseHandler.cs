using UnityEngine;

public class PauseHandler : MonoBehaviour
{
    [SerializeField] private GameObject _menuPanel;

    public void ActivateMenu()
    {
        _menuPanel.SetActive(true);
        Time.timeScale = 0;
    }
}
