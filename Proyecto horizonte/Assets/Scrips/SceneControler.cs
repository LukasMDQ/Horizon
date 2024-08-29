
using UnityEngine.SceneManagement;
using UnityEngine;

public class SceneControler : MonoBehaviour
{
   
    void Update()
    {
        ChangeLevel();
        BackLevel();
        AudioPlayer();
        PauseAudio();

    }
    public void ChangeLevel()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1)) 
        {
            SceneManager.LoadScene(1);
        }
    }
    public void BackLevel()
    {
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            SceneManager.LoadScene(0);
        }
    }
    public void AudioPlayer()
    {
        if (Input.GetKeyDown(KeyCode.O))
        {
            TestSound.PlayAudio();
        }
    }
    public void PauseAudio()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            TestSound.PausarAudio();
        }
    }
}
