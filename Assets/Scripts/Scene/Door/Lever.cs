using UnityEngine;
using System;

// TP2 - Lidia, Paiva - Ahumada, Leandro
// La lever
public class Lever : MonoBehaviour, IInteractable
{
    public event Action OnLeverActivated;
    public event Action OnLeverDeactivated;

    public AudioSource audioSource;
    [SerializeField]
    private AudioClip blockSoundEffect;
    [SerializeField]
    private AudioClip activateSoundEffect;
    [SerializeField]
    private AudioClip deactivateSoundEffect;
    private bool isActivated = false;
    [SerializeField]
    private bool isBlockedDeactivated = false;

    public Animator leverAnimator;

    private static readonly int LeverActivated = Animator.StringToHash("Activate");
    private static readonly int LeverDeactivated = Animator.StringToHash("Deactivate");

    public void Interact()
    {
        if (!isActivated)
        {
            isActivated = true;
            OnLeverActivated?.Invoke();
            leverAnimator.SetTrigger(LeverActivated);
            if (activateSoundEffect) audioSource.PlayOneShot(activateSoundEffect);
        } else
        {
            if (!isBlockedDeactivated)
            {
                isActivated = false;
                OnLeverDeactivated?.Invoke();
                leverAnimator.SetTrigger(LeverDeactivated);
                if (deactivateSoundEffect) audioSource.PlayOneShot(deactivateSoundEffect);
            } else
            {
                if (blockSoundEffect) audioSource.PlayOneShot(blockSoundEffect);
            }           
        }
    }
}
