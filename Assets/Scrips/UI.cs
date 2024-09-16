using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI : MonoBehaviour
{    
    private Slider slider;    
    public TextMeshProUGUI textMesh;  
   
    private void Start()
    {
        textMesh = GetComponent<TextMeshProUGUI>();
        slider = GetComponent<Slider>();//barraHp
        
    }   
    //---------------LOGICA BARRA HP--------------//    
    public void ChangeMaxHp(float maxLife)
    {
        slider.maxValue = maxLife;       
    }    

    public void ChangeCurrentHp(float curLife) 
    {
        slider.value = curLife;
    }    
    public void StartHpBar(float curLife)
    {
        ChangeMaxHp(curLife);
        ChangeCurrentHp( curLife);
    }    

}
