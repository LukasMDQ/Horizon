using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Button : MonoBehaviour
{

    public void PlayGame()
    {
        SceneManager.LoadScene(1);
        Time.timeScale = 1;
    }
    public void MainGame()
    {
        SceneManager.LoadScene(0);
        Time.timeScale = 1;
    }

    public void QuitGame()
    {
        Application.Quit();
    }

}
