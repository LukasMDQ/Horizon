using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerSpearTrap : MonoBehaviour
{
    public SpearTrap spearTrap;

    public enum ActionTrigger
    {
        Activate,
        Deactivate
    }

    [SerializeField] private ActionTrigger actionTrigger;
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            if (actionTrigger == ActionTrigger.Activate)
            {
                ActivateTrap();
            }
            else if (actionTrigger == ActionTrigger.Deactivate)
            {
                DeactivateTrap();
            }
        }
    }

    private void ActivateTrap()
    {
        if (spearTrap != null && !spearTrap.isActiveTrap)
        {
            spearTrap.isActiveTrap = true;
        }
    }

    private void DeactivateTrap()
    {
        if (spearTrap != null && spearTrap.isActiveTrap)
        {
            spearTrap.isActiveTrap = false;
        }
    }
}
