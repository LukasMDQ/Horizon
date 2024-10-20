using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

// ReSharper disable once InconsistentNaming
public class UI : MonoBehaviour
{    
    [SerializeField] // ReSharper disable once InconsistentNaming
    private Slider _slider;    
    public TextMeshProUGUI textMesh;  
   
    private void Start()
    {
        textMesh = GetComponent<TextMeshProUGUI>();
        _slider = GetComponent<Slider>();//barraHp
        
    }   
    //---------------LOGICA BARRA HP--------------//    
    public void ChangeMaxHp(float maxLife)
    {
        _slider.maxValue = maxLife;       
    }    

    public void ChangeCurrentHp(float curLife) 
    {
        _slider.value = curLife;
    }    
    public void StartHpBar(float curLife)
    {
        ChangeMaxHp(curLife);
        ChangeCurrentHp( curLife);
    }    

}
