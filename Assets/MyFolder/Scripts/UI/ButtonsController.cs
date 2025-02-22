using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonsController : MonoBehaviour
{
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
}
