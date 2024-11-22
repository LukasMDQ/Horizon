using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

// TP 2 - Ahumada, Leandro
// Script de Controladora de Audio, Set de Volumen.

public class AudioManager : MonoBehaviour
{

    [SerializeField] //Audio Mixer
    AudioMixer _audioMixer;

    [SerializeField] //Máximo y minimo de volúmen.
    float _minDB = -80;
    [SerializeField]
    float _maxDB = 0;

    [SerializeField] //Set inicial
    [Range(0, 1)] public float master = 1f;
    [Range(0, 1)] public float BGM = 1f;
    [Range(0, 1)] public float SFX = 1f;

    void Update() //Setea el Volumen en cada Frame.
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
