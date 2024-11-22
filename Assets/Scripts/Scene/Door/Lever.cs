using UnityEngine;
using System;

// Lidia Paiva
public class Lever : MonoBehaviour, IInteractable
{
    public event Action OnLeverActivated;
    public event Action OnLeverDeactivated;

    private bool isActivated = false;

    public void Interact()
    {
        if (!isActivated)
        {
            isActivated = true;
            OnLeverActivated?.Invoke();
        } else
        {
            isActivated = false;
            OnLeverDeactivated?.Invoke();
        }
    }
}
