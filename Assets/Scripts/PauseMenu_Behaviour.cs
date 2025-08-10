using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu_Behaviour : MonoBehaviour
{
    public GameObject PauseMenu;
    public Player_Behaviour PlayerObject;

    public void PauseGame()
    {
        if (PlayerObject != null)
        {
            PauseMenu.SetActive(true);
            Time.timeScale = 0f;
            PlayerObject.SetMoveCamState(false);
            PlayerObject.SetCursorState(true);
            
        }
    }
    public void ResumeGame()
    {
        if (PlayerObject != null)
        {
            PauseMenu.SetActive(false);
            Time.timeScale = 1f;
            PlayerObject.SetMoveCamState(true);
            PlayerObject.SetCursorState(false);
        }
    }
    public void TutorialRecap()
    {
        
    }
    public void QuitGame()
    {
        Application.Quit();
    }
}
