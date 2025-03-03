using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class DevModeActions : MonoBehaviour
{
    public TMP_Dropdown sceneDropdown;
    private static readonly List<string> list = new List<string>();
    private readonly List<string> sceneNames = list;


    void Start()
    {
        PopulateDropdown();
    }

    void PopulateDropdown()
    {
        sceneDropdown.ClearOptions();
        int sceneCount = SceneManager.sceneCountInBuildSettings;

        for (int i = 0; i < sceneCount; i++)
        {
            string scenePath = SceneUtility.GetScenePathByBuildIndex(i);
            string sceneName = System.IO.Path.GetFileNameWithoutExtension(scenePath);
            sceneNames.Add(sceneName);
        }

        sceneDropdown.AddOptions(sceneNames);
        sceneDropdown.onValueChanged.AddListener(delegate { OnSceneSelected(sceneDropdown.value); });
    }

    public void OnSceneSelected(int index)
    {
        SceneManager.LoadScene(sceneNames[index]);
    }

    public void ToggleInvulnerable(bool checkToggle)
    {
        PlayerStatsManager.isInvulnerable = checkToggle;
        Debug.Log($"Value:{checkToggle}");
    }

    public void ChangePlayerDamage(float dmgValue)
    {
        PlayerStatsManager.Damage = (int)dmgValue;
        Debug.Log($"dmgPlayer:{PlayerStatsManager.Damage}");
    }

    public void ToggleAllJewels(bool checkToggle)
    {
        if (checkToggle)
        {
            PlayerStatsManager.jewels = 3;
        }
        else
        {
            PlayerStatsManager.jewels = 0;
        }
    }
}
