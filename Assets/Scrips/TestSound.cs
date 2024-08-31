using UnityEngine.SceneManagement;
using UnityEngine;

public class TestSound : MonoBehaviour
{
    public static TestSound instancia;
    AudioSource _audsource;
    
    void Awake()
    {
        
        if(TestSound.instancia == null)
        {
            TestSound.instancia = this;
            DontDestroyOnLoad(gameObject);
            _audsource= GetComponent<AudioSource>();
        }
        else 
        {
            Destroy(gameObject);
        }
    }
    public static void PausarAudio()
    {
       instancia._audsource.Pause();
    }
    public static void PlayAudio()
    {
        instancia._audsource.UnPause();
    }

}
