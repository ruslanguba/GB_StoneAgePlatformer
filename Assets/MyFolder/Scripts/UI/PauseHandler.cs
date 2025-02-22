using UnityEngine;

public class PauseHandler : MonoBehaviour
{
    [SerializeField] private GameObject _menuPanel;
    [SerializeField] private ButtonsController _buttonsController => GetComponent<ButtonsController>();
    private bool _isPaused = false;
     

    private void OnEnable()
    {
        _buttonsController.OnContinue += Continue;
    }
    private void OnDisable()
    {
        _buttonsController.OnContinue -= Continue;
    }
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if(!_isPaused)
            {
                ActivateMenu();
            }
            else
            {
                Continue();
            }
        }
    }
    public void ActivateMenu()
    {
        _menuPanel.SetActive(true);
        _isPaused = true;
        Time.timeScale = 0;
    }

    public void Continue()
    {
        _menuPanel.SetActive(false);
        _isPaused = false;
        Time.timeScale = 1;
    }
}
