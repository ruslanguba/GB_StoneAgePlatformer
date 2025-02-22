using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonsController : MonoBehaviour
{
    public event Action OnContinue;
    public void OnStartGameplayClick()
    {
        SceneManager.LoadScene(1);
    }

    public void OnExitClick()
    {
        Application.Quit();
    }

    public void OnRestartClick()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void OnExitToMenuMenuClick()
    {
        SceneManager.LoadScene(0);
    }

    public void OnContinueClick()
    {
        OnContinue?.Invoke();
    }
}
