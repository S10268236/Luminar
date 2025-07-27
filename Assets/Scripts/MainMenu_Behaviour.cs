using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu_Behaviour : MonoBehaviour
{
    public void PlayGame()
    {
        SceneManager.LoadSceneAsync(1);
    }
    public void QuitGame()
    {
        Application.Quit();
    }
}
