using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponChanger : MonoBehaviour
{
    public GameObject[] weapons;
    private int _selectedWeapon;

    private void Start()
    {
        weapons[0].SetActive(true);
        _selectedWeapon = 0;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            _selectedWeapon--;
            if (_selectedWeapon < 0) _selectedWeapon = weapons.Length - 1;
            
            ActivateWeapon(_selectedWeapon);
        }
        else if (Input.GetKeyDown(KeyCode.E))
        {
            _selectedWeapon++;
            if (_selectedWeapon > weapons.Length - 1) _selectedWeapon = 0;
            
            ActivateWeapon(_selectedWeapon);
        }
    }

    private void ActivateWeapon(int index)
    {
        foreach (var weapon in weapons)
        {
            weapon.SetActive(false);
        }
        
        weapons[index].SetActive(true);
    }
}