using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
