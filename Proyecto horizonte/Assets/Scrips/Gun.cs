using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    [SerializeField] GameObject _inpactEffect, _inpactEffect2, _FlashEffect, _instancePoint;
    [SerializeField] AudioSource _audSource;
    [SerializeField] AudioClip _Clip_Fire, _Clip_Impact, _Clip_ObjectInpact;
    [SerializeField] float _range = default;//100
    [SerializeField] Camera _playerCam;
    public Nemesis nemesis;   

    
    void Update()
    {
        FireInput();
    }
    void FireInput()
    {
        if (Input.GetMouseButtonDown(0)) 
        {           
            Fire();
            flash();
        }
    } 
    void Fire()
    {        

        RaycastHit hit;
        AudioDisparo( _Clip_Fire );
        if (Physics.Raycast(_playerCam.transform.position, _playerCam.transform.forward, out hit, _range))
        {
            if (hit.transform.tag == "Enemy")
            {
                hit.transform.gameObject.GetComponent<VidaEnemigo>().vida -= 10;                
                AudioDisparo (_Clip_Impact );
                Debug.Log ("EN EL BLANCO !!!");
                GameObject InpactoGO = Instantiate(_inpactEffect,hit.point,Quaternion.LookRotation(hit.normal));                
                Destroy(InpactoGO,2f);              
            }
            if (hit.transform.tag == "Floor")
            {
                AudioDisparo(_Clip_ObjectInpact);
                
                GameObject InpactoGO = Instantiate(_inpactEffect2, hit.point, Quaternion.LookRotation(hit.normal));
                Destroy(InpactoGO, 2f);
            }
            if (hit.transform.tag == "Wall")
            {
                AudioDisparo(_Clip_Fire);

                GameObject InpactoGO = Instantiate(_inpactEffect2, hit.point, Quaternion.LookRotation(hit.normal));
                Destroy(InpactoGO, 2f);
            }
        }        
    }
    private void flash()
    {
        Instantiate(_FlashEffect, _instancePoint.transform.position, Quaternion.identity);
    }
    void AudioDisparo (AudioClip _Clip_Test)
    {
        _audSource.clip= _Clip_Test;
        _audSource.Play();
    }
    
}
