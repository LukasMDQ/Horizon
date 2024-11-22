using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// TP 2 - Ahumada, Leandro
// Script de Menú de Pausa, Frenar y Reanudar.

public class Pause : MonoBehaviour
{
    public GameObject pauseMenu;
    public bool isPaused;  
    void Start() //Siempre que entra a un nivel, el menú de Pausa está desactivado.
    {
        pauseMenu.SetActive(false);
    }

    void Update() //Pausa
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }
    }

    public void PauseGame() //Pausa el juego.
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        isPaused = true;
    }

    public void ResumeGame() //Reanuda el Juego.
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        isPaused = false;
    }
    public void ResumeGameFromMenu() //Método especial que se ejecuta cuando se vuelve al menú principal. (Reanuda el juego desde la pausa, mantiene el cursor visible y desbloqueado)
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        isPaused = false;
    }
}
