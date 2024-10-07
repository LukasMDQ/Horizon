using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FountainController : MonoBehaviour
{
    [SerializeField] Material lightingWater;
    [SerializeField] Material opaqueWater;
    [SerializeField] Renderer[] waterRenders;
    private bool isPlayerInTrigger = false;

    public string turnOnColorLight = "#2EAEB7";

    public string turnOffColorLight = "#125C61";

    void Start()
    {
        ChangeLightColor(turnOffColorLight);
    }
    void Update()
    {
        if (isPlayerInTrigger && Input.GetKeyDown(KeyCode.F))
        {
            DeactivateWater();
        }
    }
    public void ChangeLightColor(string hexColor)
    {
        Color newColor;
        if (ColorUtility.TryParseHtmlString(hexColor, out newColor))
        {
            Light[] lights = GetComponentsInChildren<Light>();
            foreach (Light light in lights)
            {
                if (light.type == LightType.Point)
                {
                    light.color = newColor;
                }
            }
        }
    }
    public void ActivateWater()
    {
        ChangeLightColor(turnOnColorLight);
        foreach (Renderer render in waterRenders)
        {
            render.sharedMaterial = lightingWater;
        }
    }

    public void DeactivateWater()
    {
        ChangeLightColor(turnOffColorLight);
        foreach (Renderer render in waterRenders)
        {
            render.sharedMaterial = opaqueWater;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInTrigger = true;
            ActivateWater();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInTrigger = false;
            DeactivateWater();
        }
    }

}
