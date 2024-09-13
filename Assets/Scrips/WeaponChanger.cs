using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponChanger : MonoBehaviour
{
    public GameObject[] weapons;
    public int selectedWeapon; //Public para el Animator

    private void Start()
    {
        weapons[0].SetActive(true);
        selectedWeapon = 0;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            selectedWeapon--;
            if (selectedWeapon < 0) selectedWeapon = weapons.Length - 1;
            
            ActivateWeapon(selectedWeapon);
        }
        else if (Input.GetKeyDown(KeyCode.E))
        {
            selectedWeapon++;
            if (selectedWeapon > weapons.Length - 1) selectedWeapon = 0;
            
            ActivateWeapon(selectedWeapon);
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