using System.Collections;
using UnityEngine;

public class Pause : MonoBehaviour
{
    public GameObject pauseMenu;
    public GameObject devMenu;
    public GameObject hpBossUI;
    public KeyCode toggleKey = KeyCode.F1;
    public bool isPaused;

    void Start()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused) ResumeGame();
            else PauseGame();
        }

        if (isPaused)
        {
            DevMode();
        }
    }

    public void PauseGame()
    {
        Time.timeScale = 0f;
        pauseMenu.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        isPaused = true;

        if(hpBossUI != null )
        {
            hpBossUI.SetActive(false);
        }
    }

    void DevMode()
    {
        if (Input.GetKeyDown(toggleKey))
        {
            devMenu.SetActive(!devMenu.activeSelf);
        }
    }

    public void ResumeGame()
    {
        Time.timeScale = 1f;
        pauseMenu.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        isPaused = false;

        if (hpBossUI != null)
        {
            hpBossUI.SetActive(true);
        }
    }

    public void ResumeGameFromMenu()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        isPaused = false;
    }
}
