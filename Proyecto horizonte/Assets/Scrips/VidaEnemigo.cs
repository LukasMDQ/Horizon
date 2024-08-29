using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VidaEnemigo : MonoBehaviour
{
    public float vida = 100;
    public GameObject efectoMuerte;
    public AudioSource _audSource;
    public AudioClip _Clip_Muerte;
    public Nemesis nemesis;

    void Update()
    {
        muerteEnemigo();
        //hostilidad();
    }
    void hostilidad()
    {
        if (vida==90)
        {
            nemesis.agresion();
        }
    }
    
    void muerteEnemigo()
    {
        if (vida<=0)
        {
            AudioSound(_Clip_Muerte);
            GameObject explosionEnemigo  = Instantiate(efectoMuerte,transform.position,transform.rotation);
            Destroy(explosionEnemigo,4);
            Destroy(gameObject);            
        }
    }
    void AudioSound(AudioClip _Clip_Test)
    {
        _audSource.clip = _Clip_Test;
        _audSource.Play();
    }
}
