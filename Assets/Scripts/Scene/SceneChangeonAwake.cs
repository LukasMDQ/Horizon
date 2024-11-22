using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// TP 2 - Ahumada, Leandro
// Script cambio de Escena en Awake (Usado en Cinemática/Trigger de Animación).

public class SceneChangeonAwake : MonoBehaviour
{
    public string sceneName;
    void OnEnable()
    {
        SceneManager.LoadScene(sceneName);
    }
}
