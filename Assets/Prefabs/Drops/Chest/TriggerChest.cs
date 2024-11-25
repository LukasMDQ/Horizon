using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerChest : MonoBehaviour, IInteractable
{
    private Animator _chestAnimator;
    private bool _isActive;
    public GameObject chestBaseCollider;

    private void Start()
    {
        _chestAnimator = GetComponent<Animator>();
    }

    private void OnActived()
    {
        _isActive = true;
        _chestAnimator.SetTrigger("OpenedChest");
        if(chestBaseCollider)
        {
            chestBaseCollider.SetActive(false);

        }
    }

    private void OnDeactivated()
    {
        _chestAnimator.SetTrigger("ClosedChest");
        _isActive = false;
        if(chestBaseCollider)
        {
            chestBaseCollider.SetActive(true);
        }
    }

    public void Interact()
    {
        if (!_isActive)
        {
            OnActived();
        } else
        {
            OnDeactivated();
        }
    }
}
