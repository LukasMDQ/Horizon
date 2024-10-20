using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Cooldown : MonoBehaviour
{
    [SerializeField] // ReSharper disable once InconsistentNaming
    private Slider _slider;

    private void Start()
    {
        if (!_slider)
        {
            _slider = GetComponent<Slider>();
        }
    }
    
    //---------------COOLDOWN SHIELD LOGIC--------------//    
    private void ChangeMaxShield(float maxShield)
    {
        _slider.maxValue = maxShield;
    }

    private void ChangeCurrentShield(float currentShield)
    {
        _slider.value = currentShield;
    }
    
    public void StartingShield(float currentShield)
    {
        ChangeMaxShield(currentShield);
        ChangeCurrentShield(currentShield);
    }
}