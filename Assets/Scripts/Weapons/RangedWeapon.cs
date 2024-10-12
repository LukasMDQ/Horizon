using System;
using UnityEngine;
// ReSharper disable InconsistentNaming

namespace Weapons
{
    public abstract class RangedWeapon : Weapon
    {
        [SerializeField] private GameObject _flashEffect;
        [SerializeField] private Transform _spawnPoint;
        [SerializeField] private GameObject _bullet;
        public AudioSource audioSource;
        public AudioClip noAmmoClip;
        public AudioClip reloadClip;

        protected int ammo;
        public int maxAmmo;

        protected virtual void Start()
        {
            ammo = maxAmmo;
        }

        public override void Attack()
        {
            if (ammo > 0)
            {
                ammo--;
                
                var position = _spawnPoint.position;
            
                Instantiate(_bullet, position, _spawnPoint.rotation);
            
                Instantiate(_flashEffect, position, Quaternion.identity); // VFX and sound
                
                animationWeapons.AnimateThisRanged(this);
            }
            else
            {
                audioSource.PlayOneShot(noAmmoClip);
            }
        }

        public abstract void Reload();
    }
}