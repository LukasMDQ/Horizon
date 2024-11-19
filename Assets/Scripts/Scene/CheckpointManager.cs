using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.SceneManagement;

public class CheckpointManager : MonoBehaviour
{
    public Rewind[] rewinds;
    private int lastSavedSceneIndex;
    private Vector3 lastSavedPosition;

    // Save scene index and player position
    public void SaveElements(Vector3 playerPosition)
    {
        lastSavedSceneIndex = SceneManager.GetActiveScene().buildIndex;
        lastSavedPosition = playerPosition;

        foreach (var item in rewinds)
        {
            item.Save();
        }
        PlayerPrefs.SetInt("SavedScene", lastSavedSceneIndex);
        PlayerPrefs.SetFloat("SavedPositionX", lastSavedPosition.x);
        PlayerPrefs.SetFloat("SavedPositionY", lastSavedPosition.y);
        PlayerPrefs.SetFloat("SavedPositionZ", lastSavedPosition.z);
    }

    public (int, Vector3) GetLastSavedData()
    {
        return (lastSavedSceneIndex, lastSavedPosition);
    }
}