using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TooltipTrigger : MonoBehaviour
{
    private Transform playerTransform;
    public float maxTooltipDistance = 3.0f;
    public string content;
    public string header;

    private void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void Update()
    {
        float distance = Vector3.Distance(playerTransform.position, transform.position);
        if (distance < maxTooltipDistance)
        {
            TooltipSystem.Show(content, transform.position, header);
        }
        else
        {
            TooltipSystem.Hide();
        }
    }
}
