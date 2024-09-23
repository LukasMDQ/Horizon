using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// ReSharper disable once CheckNamespace
public class SkillsCooldownFeedback : MonoBehaviour
{
    [SerializeField] // ReSharper disable once InconsistentNaming
    private Slider _slider;

    private void Start()
    {
        if (!_slider)
        {
            _slider = GetComponent<Slider>();
        }

        _slider.value = _slider.maxValue;
        _slider.gameObject.SetActive(false);
    }

    public IEnumerator ActivateCooldown(float cooldown)
    {
        _slider.gameObject.SetActive(true);
        
        while (_slider.value > 0)
        {
            _slider.value = Mathf.Clamp(_slider.value - Time.deltaTime / cooldown, 0, 1);
            yield return null;
        }
        
        _slider.gameObject.SetActive(false);
        _slider.value = 1;
    }
}