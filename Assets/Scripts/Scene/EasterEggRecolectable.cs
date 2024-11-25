using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EasterEggRecolectable : MonoBehaviour, IInteractable
{
    void IInteractable.Interact()
    {
        PlayerStatsManager.easterEggAcc++;
        Debug.Log($"Player stats easter eggs {PlayerStatsManager.easterEggAcc}");
        gameObject.SetActive(false);
        TooltipSystem.Hide();
    }
}
