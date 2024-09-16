using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class cooldown : MonoBehaviour
{
    private Slider slider;
    void Start()
    {
        slider = GetComponent<Slider>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    //---------------LOGICA COOLDOWN SHIELD--------------//    
    public void ChangeMaxShield(float maxshield)
    {
        slider.maxValue = maxshield;
    }
    public void ChangeCurshield(float curshield)
    {
        slider.value = curshield;
    }
    public void Startshield(float curshield)
    {
        ChangeMaxShield(curshield);
        ChangeCurshield(curshield);
    }
}
