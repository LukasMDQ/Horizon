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

    void Awake()
    {
        ActivateWater();
    }
    void Update()
    {
        if (isPlayerInTrigger && Input.GetKeyDown(KeyCode.F))
        {
            DeactivateWater();
            HealPlayer();
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

    private void HealPlayer()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player == null) return;
        Stats stats = player.GetComponent<Stats>();
        int playerMaxLife = (int)stats.maxHp;
        stats.Heal(playerMaxLife);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInTrigger = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            ActivateWater();
            isPlayerInTrigger = false;
        }
    }

}
