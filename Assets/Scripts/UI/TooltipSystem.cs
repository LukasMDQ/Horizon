using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TooltipSystem : MonoBehaviour
{
   private static TooltipSystem current;

    public Tooltip tooltip;
    private Camera mainCamera;

    private void Awake()
    {
        current = this;
        mainCamera = Camera.main;
    }

    public static void Show(string content, Vector3 worldPosition, string header = "")
    {
        Vector3 screenPosition = current.mainCamera.WorldToScreenPoint(worldPosition);
        current.tooltip.SetText(content, header);
        current.tooltip.transform.position = screenPosition;

        current.tooltip.gameObject.SetActive(true);

    }

    public static void Hide()
    {
        current.tooltip.gameObject.SetActive(false);
    }
}
