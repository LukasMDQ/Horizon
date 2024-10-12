using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Weapons;

// ReSharper disable once CheckNamespace
public class WeaponChanger : MonoBehaviour
{
    public Weapon[] weapons;
    public int selectedWeapon; //Public para el Animator

    private void Start()
    {
        ActivateWeapon(0);
        selectedWeapon = 0;
    }

    private void ActivateWeapon(int index)
    {
        foreach (var weapon in weapons)
        {
            weapon.gameObject.SetActive(false);
        }
        
        weapons[index].gameObject.SetActive(true);
    }

    public void CycleWeaponForward()
    {
        selectedWeapon++;
        if (selectedWeapon > weapons.Length - 1) selectedWeapon = 0;
            
        ActivateWeapon(selectedWeapon);
    }

    public void CycleWeaponBackward()
    {
        selectedWeapon--;
        if (selectedWeapon < 0) selectedWeapon = weapons.Length - 1;
            
        ActivateWeapon(selectedWeapon);
    }
}