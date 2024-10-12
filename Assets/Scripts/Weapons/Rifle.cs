using UnityEngine;

namespace Weapons
{
    public class Rifle : RangedWeapon
    {
        private int _ammoInBag;
        public int maxAmmoInBag;

        protected override void Start()
        {
            base.Start();
            _ammoInBag = maxAmmoInBag;
        }

        public override void Reload()
        {
            if (ammo >= maxAmmo) return;

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
            
            audioSource.PlayOneShot(reloadClip);
        }

        public void AmmoPickUp()
        {
            _ammoInBag = maxAmmoInBag;
        }
    }
}