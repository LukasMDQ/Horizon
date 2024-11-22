using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TooltipTrigger : MonoBehaviour
{
    public string content;
    public string header;

    private void Start()
    {
        TooltipSystem.Hide();
    }

    private void OnTriggerEnter(Collider other)
    {
        Vector3 positionWithOffset = transform.position + new Vector3(0, 1f, 0);
        if (other.CompareTag("Player"))
        {
            TooltipSystem.Show(content, positionWithOffset, header);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            TooltipSystem.Hide();
        }
    }
}
