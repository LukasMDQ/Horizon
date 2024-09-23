using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour
{
    public GameObject[] armas;
    public FPSControl fPSControl;
    
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.R)) 
        {
            TirarArma();
        }
    }
    
    public void ActivarArma(int numero)
    {
        for (int i = 0 ; i < armas.Length; i++) 
        {
            TirarArma();
            armas[i].SetActive(false);
        }
        armas[numero].SetActive(true);
    }
    
    public void TirarArma()
    {        
        for (int i = 0; i < armas.Length; i++)
        {
            armas[i].SetActive(false);
        }
    }
}