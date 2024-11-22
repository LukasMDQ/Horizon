using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// TP 2 - Ahumada, Leandro - Paiva, Lidia
// Script de Transición de Escenas con Fade In, Fade Out.

public class NextScene : MonoBehaviour
{
    public string sceneName;
    public Animator animTransition;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && sceneName != "")
        {
            StartCoroutine(Transition());
        }
    }

    private IEnumerator Transition()
    {
        PlayerStatsManager.isInvulnerable = true;
        animTransition.SetTrigger("ON");
        yield return new WaitForSeconds(2f);
        PlayerStatsManager.isInvulnerable = false;
        SceneManager.LoadScene(sceneName);
    }
}
