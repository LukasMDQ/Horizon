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
            StartCoroutine(weaponChanger.CycleWeaponBackward());
        }
        else if (Input.GetKeyDown(KeyCode.E))
        {
            StartCoroutine(weaponChanger.CycleWeaponForward());
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            var rangedWeapon = weaponChanger.weapons[weaponChanger.selectedWeapon].GetComponent<RangedWeapon>();

            if (rangedWeapon)
            {
                rangedWeapon.Reload();
            }
        }

        if (Input.GetMouseButtonDown(0) && !Cursor.visible)
        {
            weaponChanger.weapons[weaponChanger.selectedWeapon].Attack();
        }
    }
}