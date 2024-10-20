using System;
using TMPro;
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

        public TextMeshProUGUI ammoUI;

        protected virtual void Awake()
        {
            ammo = maxAmmo;
        }

        private void OnEnable()
        {
            UpdateUI();
        }

        public override void Attack()
        {
            if (isAttacking) return;

            isAttacking = true;
            
            if (ammo > 0)
            {
                ammo--;
                UpdateUI();
                
                var position = _spawnPoint.position;
            
                Instantiate(_bullet, position, _spawnPoint.rotation);
            
                Instantiate(_flashEffect, position, Quaternion.identity); // VFX and sound
                
                animationWeapons.AnimateThisRanged(this);
            }
            else
            {
                audioSource.PlayOneShot(noAmmoClip);
                isAttacking = false;
            }
        }

        public abstract void Reload();

        protected void CanAttackAgain()
        {
            isAttacking = false;
        }

        protected void UpdateUI()
        {
            ammoUI.text = $"{ammo}/{maxAmmo}";
        }

        public void GetAmmo(int newAmmo)
        {
            if(newAmmo + ammo >= maxAmmo)
            {
                ammo = maxAmmo;
            } else
            {
                ammo += newAmmo;
            }
            UpdateUI();
        }
    }
}