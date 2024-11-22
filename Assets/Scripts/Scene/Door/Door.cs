using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.ProBuilder.Shapes;

// TP2 - Lidia, Paiva - Ahumada, Leandro
// Control de Puerta con una Lever

public class Door : MonoBehaviour
{
    private bool isOpen = false;
    public GameObject MetalGrate;
    public Lever linkedLever;
    private Vector3 originalPosition;

    public Animator doorAnimator;
    
    private static readonly int DoorActivated = Animator.StringToHash("ActivateDoor");
    private static readonly int DoorDeactivated = Animator.StringToHash("DeactivateDoor");

    private void Start()
    {
        //originalPosition = MetalGrate.transform.position;
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
            //MetalGrate.transform.Translate(Vector3.up * 3);
            doorAnimator.SetTrigger(DoorActivated);
            
        }
    }

    private void CloseDoor()
    {
        if(isOpen)
        {
            isOpen = false;
            doorAnimator.SetTrigger(DoorDeactivated);
            
            //MetalGrate.transform.position = originalPosition;
        }
    }
}
