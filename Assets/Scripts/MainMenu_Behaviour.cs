using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu_Behaviour : MonoBehaviour
{
    public void StartGame()
    {
        StartCoroutine(PlayGame());
    }
    IEnumerator PlayGame()
    {
        yield return new WaitForSeconds(1);
        SceneManager.LoadSceneAsync(1);
    }
    public void QuitGame()
    {
        Application.Quit();
    }
}
