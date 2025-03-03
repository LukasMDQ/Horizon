using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EasterEggRecolectable : MonoBehaviour, IInteractable
{
    public AudioSource source;
    public AudioClip clip;
    void IInteractable.Interact()
    {
        if (gameObject.activeSelf)
        {
            PlayerStatsManager.easterEggAcc++;
            RewardManager.AddGameStatCount(GameStats.PostersCollected);
            source.PlayOneShot(clip);
            Debug.Log($"Player stats easter eggs {PlayerStatsManager.easterEggAcc}");
            gameObject.SetActive(false);
            TooltipSystem.Hide();
        }
    }
}
