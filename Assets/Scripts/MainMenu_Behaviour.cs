using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu_Behaviour : MonoBehaviour
{
    /// <summary>
    /// Start coroutine
    /// </summary>
    public void StartGame()
    {
        StartCoroutine(PlayGame());
    }
    /// <summary>
    /// Wait for 1 sec for audio to finish playing
    /// </summary>
    /// <returns></returns>
    IEnumerator PlayGame()
    {
        yield return new WaitForSeconds(1);
        SceneManager.LoadSceneAsync(1);
    }
    /// <summary>
    /// Quit game
    /// </summary>
    public void QuitGame()
    {
        Application.Quit();
    }
}
