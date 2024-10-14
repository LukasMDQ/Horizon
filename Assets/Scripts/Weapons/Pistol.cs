using UnityEngine;

namespace Weapons
{
    public class Pistol : RangedWeapon
    {
        public override void Reload()
        {
            if (isAttacking || ammo >= maxAmmo) return;

            isAttacking = true;
            
            ammo = maxAmmo;
            UpdateUI();
            
            audioSource.PlayOneShot(reloadClip);
            Invoke(nameof(CanAttackAgain), 0.4f);
        }
    }
}