using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// TP 2 - Ahumada, Leandro
// Script de Transición de Escena específico de Cinemática (Sin Fade In, Fade Out).

public class CinematicEndingTransition : MonoBehaviour
{
    public string sceneName;

    void OnTriggerEnter(Collider other) //Al tocar un trigger, cambia de escena
    {

        if (other.CompareTag("Player") && sceneName != "")
        {
            if (PlayerStatsManager.easterEggAcc >= 3) {
                SceneManager.LoadScene("EndingEasterEgg");
            } else
            {
                SceneManager.LoadScene(sceneName);
            }
        }
    }
}
