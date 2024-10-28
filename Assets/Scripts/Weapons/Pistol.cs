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
            UpdateUIReload();
            
            audioSource.PlayOneShot(reloadClip);
            Invoke(nameof(CanAttackAgain), 0.4f);
        }

        protected override void UpdateUI()
        {
            base.UpdateUI();
            ammoInBagUI.text = "Unlimited";
        }

        public void UpdateUIReload()
        {
            reloadUI.alpha = 0f;
        }
    }
}