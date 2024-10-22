using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerChest : MonoBehaviour
{
    private Animator _chestAnimator;

    private void Start()
    {
        _chestAnimator = GetComponent<Animator>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) {
            _chestAnimator.SetTrigger("OpenedChest");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _chestAnimator.SetTrigger("ClosedChest");
        }
    }
}
