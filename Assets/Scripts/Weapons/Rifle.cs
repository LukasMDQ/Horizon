using UnityEngine;

namespace Weapons
{
    public class Rifle : RangedWeapon
    {
        private int _ammoInBag;
        public int maxAmmoInBag;

        protected override void Awake()
        {
            base.Awake();
            _ammoInBag = maxAmmoInBag;
        }

        public override void Reload()
        {
            if (isAttacking || ammo >= maxAmmo) return;

            isAttacking = true;

            var numAux = maxAmmo - ammo;

            if (_ammoInBag >= numAux)
            {
                _ammoInBag -= numAux;
                ammo += numAux;
            }
            else
            {
                ammo += _ammoInBag;
                _ammoInBag = 0;
            }
            UpdateUI();
            UpdateUIReload();
            audioSource.PlayOneShot(reloadClip);
            Invoke(nameof(CanAttackAgain), 0.4f);
        }
        public override void GetAmmo(int newAmmo)
        {
            if (newAmmo + _ammoInBag >= maxAmmoInBag)
            {
                _ammoInBag = maxAmmoInBag;
            }
            else
            {
                _ammoInBag += newAmmo;
            }
            UpdateUI();
        }

        protected override void UpdateUI()
        {
            base.UpdateUI();
            ammoInBagUI.text = $"{_ammoInBag}/{maxAmmoInBag}";
        }

        public void AmmoPickUp()
        {
            _ammoInBag = maxAmmoInBag;
        }
    }
}