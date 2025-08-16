using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu_Behaviour : MonoBehaviour
{
    //Set pause menu object
    public GameObject PauseMenu;
    //Access player script
    public Player_Behaviour PlayerObject;
    /// <summary>
    /// Pause time and stop player movement and camera
    ///  and activate cursor
    /// </summary>
    public void PauseGame()
    {
        if (PlayerObject != null)
        {
            PauseMenu.SetActive(true);
            Time.timeScale = 0f;
            PlayerObject.SetMoveCamState(false);//Stop player movement and camera
            PlayerObject.SetCursorState(true);

        }
    }
    /// <summary>
    /// Resume game 
    /// </summary>
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
    /// <summary>
    /// Quit game
    /// </summary>
    public void QuitGame()
    {
        Application.Quit();
    }
}
