using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.ProBuilder.Shapes;

public class Door : MonoBehaviour
{
    private bool isOpen = false;
    public GameObject MetalGrate;
    public Lever linkedLever;
    private Vector3 originalPosition;

    private void Start()
    {
        originalPosition = MetalGrate.transform.position;
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
            MetalGrate.transform.Translate(Vector3.up * 3);
        }
    }

    private void CloseDoor()
    {
        if(isOpen)
        {
            isOpen = false;
            MetalGrate.transform.position = originalPosition;
        }
    }
}
