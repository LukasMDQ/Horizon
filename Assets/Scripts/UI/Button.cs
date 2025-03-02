using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class Button : MonoBehaviour
{
    public GameObject player;
    public Rewind[] rewinds;
    public Animator animTransition;
    public string sceneName;
    public float transitionDuration;
    public void PlayGame()
    {
        StartCoroutine(Transition());
        Time.timeScale = 1;

        PlayerStatsManager.HP = PlayerStatsManager.MaxHP;
        PlayerStatsManager.jewels = 0;
        PlayerStatsManager.easterEggAcc = 0;
        PlayerPrefs.DeleteKey("SavedScene");
        PlayerPrefs.DeleteKey("SavedPositionX");
        PlayerPrefs.DeleteKey("SavedPositionY");
        PlayerPrefs.DeleteKey("SavedPositionZ");

    }

    public void MainGame()
    {
        SceneManager.LoadScene(0);
        Time.timeScale = 1;
    }
    public void Prototype()
    {
        SceneManager.LoadScene(8);
        Time.timeScale = 1;
    }

    public void QuitGame()
    {
        Application.Quit();
    }


    public void LoadGame()
    {
        PlayerStatsManager.easterEggAcc = 0;
        if (SceneManager.GetActiveScene().name != "Lvl1Remastered")
        {
            // Load saved scene index and position from PlayerPrefs
            int savedSceneIndex = PlayerPrefs.GetInt("SavedScene", SceneManager.GetActiveScene().buildIndex);
            float x = PlayerPrefs.GetFloat("SavedPositionX", player.transform.position.x);
            float y = PlayerPrefs.GetFloat("SavedPositionY", player.transform.position.y);
            float z = PlayerPrefs.GetFloat("SavedPositionZ", player.transform.position.z);

            Vector3 savedPosition = new Vector3(x, y, z);

            // Load the saved scene and then set the player's position
            SceneManager.LoadScene(savedSceneIndex);
            player.transform.position = savedPosition;
        } else
        {
            SceneManager.LoadScene("Lvl1Remastered");
        }

    }

    private IEnumerator Transition()
    {
        animTransition.SetTrigger("ON");
        yield return new WaitForSeconds(transitionDuration);
        SceneManager.LoadScene(sceneName);
    }   

}
