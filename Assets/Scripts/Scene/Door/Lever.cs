using UnityEngine;
using System;

// TP2 - Lidia, Paiva - Ahumada, Leandro
// La lever
public class Lever : MonoBehaviour, IInteractable
{
    public event Action OnLeverActivated;
    public event Action OnLeverDeactivated;

    private bool isActivated = false;

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
        } else
        {
            isActivated = false;
            OnLeverDeactivated?.Invoke();
            leverAnimator.SetTrigger(LeverDeactivated);
        }
    }
}
