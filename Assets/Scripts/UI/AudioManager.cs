using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{

    [SerializeField]
    AudioMixer _audioMixer;

    [SerializeField]
    float _minDB = -80;
    [SerializeField]
    float _maxDB = 0;

    [SerializeField]
    [Range(0, 1)] public float master = 1f;
    [Range(0, 1)] public float BGM = 1f;
    [Range(0, 1)] public float SFX = 1f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        _audioMixer.SetFloat("VolumeMaster", Mathf.Lerp(_minDB, _maxDB, master));
        _audioMixer.SetFloat("VolumeMusic", Mathf.Lerp(_minDB, _maxDB, BGM));
        _audioMixer.SetFloat("VolumeSFX", Mathf.Lerp(_minDB, _maxDB, SFX));
    }

    #region Metodos Set
    // -------------- Métodos Set ------------------- 
    public void SetMaster(float newMaster)
    {
        master = newMaster;
    }

    public void SetBGM(float newBGM)
    {
        BGM = newBGM;
    }

    public void SetSFX(float newSFX)
    {
        SFX = newSFX;
    }
    #endregion

}
