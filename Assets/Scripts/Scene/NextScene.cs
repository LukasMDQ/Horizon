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
        animTransition.SetTrigger("ON");
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene(sceneName);
    }
}
