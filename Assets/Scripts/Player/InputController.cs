using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Weapons;

// ReSharper disable once CheckNamespace
public class InputController : MonoBehaviour
{
    public WeaponChanger weaponChanger;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            weaponChanger.CycleWeaponBackward();
        }
        else if (Input.GetKeyDown(KeyCode.E))
        {
            weaponChanger.CycleWeaponForward();
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            var rangedWeapon = weaponChanger.weapons[weaponChanger.selectedWeapon].GetComponent<RangedWeapon>();

            if (rangedWeapon)
            {
                rangedWeapon.Reload();
            }
        }

        if (Input.GetMouseButtonDown(0))
        {
            weaponChanger.weapons[weaponChanger.selectedWeapon].Attack();
        }
    }
}