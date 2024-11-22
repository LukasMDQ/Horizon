using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.ProBuilder.Shapes;

//Lidia Paiva
public class Door : MonoBehaviour
{
    private bool isOpen = false;
    public GameObject MetalGrate;
    public Lever linkedLever;

    public Animator doorAnimator;
    public Animator leverAnimator;

    private static readonly int LeverActivated = Animator.StringToHash("Activate");
    private static readonly int LeverDeactivated = Animator.StringToHash("Deactivate");
    private static readonly int DoorActivated = Animator.StringToHash("ActivateDoor");
    private static readonly int DoorDeactivated = Animator.StringToHash("DeactivateDoor");

    private void Start()
    {
        if (linkedLever != null)
        {
            linkedLever.OnLeverActivated += OpenDoor;
            linkedLever.OnLeverDeactivated += CloseDoor;
            
        }
    }
    public void SubscribeToLever(Lever lever)
    {
        lever.OnLeverActivated += OpenDoor;
    }

    private void OpenDoor()
    {
        if (!isOpen)
        {
            isOpen = true;
            doorAnimator.SetTrigger(DoorActivated);
            leverAnimator.SetTrigger(LeverActivated);
        }
    }

    private void CloseDoor()
    {
        if(isOpen)
        {
            isOpen = false;
            doorAnimator.SetTrigger(DoorDeactivated);
            leverAnimator.SetTrigger(LeverDeactivated);
        }
    }
}
