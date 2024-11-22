using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Lidia Paiva
public class FountainController : MonoBehaviour, IInteractable
{
    [SerializeField] Material lightingWater;
    [SerializeField] Material opaqueWater;
    [SerializeField] Renderer[] waterRenders;
    [SerializeField] ChaliceAnimation animationController;
    private CheckpointManager checkpointManager;

    public string turnOnColorLight = "#2EAEB7";

    public string turnOffColorLight = "#125C61";

    void Awake()
    {
        ActivateWater();
    }

    private void Start()
    {
         checkpointManager = GetComponent<CheckpointManager>();
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

    private void UseChargeChalice()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player == null) return;
        ChaliceController chaliceController = player.GetComponent<ChaliceController>();
        animationController.FountainUse();
        chaliceController.ChargeChalice();
    }

    public void Interact()
    {
        DeactivateWater();
        UseChargeChalice();

        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            checkpointManager.SaveElements(player.transform.position);
        }
    }

}
