using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Weapons;

//Lidia Paiva
//Lucas Hernandez
public class AmmoDrop : Drop
{
    public override void ApplyDropEffect(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GiveAmmo(other);
            base.ApplyDropEffect(other);
        }
    }

    private void GiveAmmo(Collider other)
    {
        RangedWeapon ammoWeapon = other.GetComponentInChildren<RangedWeapon>();

        if (ammoWeapon != null)
        {
             ammoWeapon.GetAmmo(ammoWeapon.maxAmmo);
        }
    }


}
