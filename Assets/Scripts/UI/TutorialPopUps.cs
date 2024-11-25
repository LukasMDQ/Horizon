using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialPopUps : MonoBehaviour
{
    public GameObject dialogue;

    void Start()
    {
            dialogue.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            dialogue.SetActive(true);
        }
    }
}
