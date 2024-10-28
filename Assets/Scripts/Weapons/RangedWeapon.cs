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
        [SerializeField] public CanvasGroup reloadUI;
        public AudioSource audioSource;
        public AudioClip noAmmoClip;
        public AudioClip reloadClip;

        protected int ammo;
        public int maxAmmo;

        public TextMeshProUGUI ammoInWeaponUI;
        public TextMeshProUGUI ammoInBagUI;

        protected virtual void Awake()
        {
            ammo = maxAmmo;
        }

        private void OnEnable()
        {
            UpdateUI();
        }

        private void OnDisable()
        {
            ClearUI();
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
                reloadUI.alpha = 1f;
                isAttacking = false;
            }
        }

        public abstract void Reload();

        protected void CanAttackAgain()
        {
            isAttacking = false;
        }

        protected virtual void UpdateUI()
        {
            ammoInWeaponUI.text = $"{ammo}/{maxAmmo}";
        }

        private void ClearUI()
        {
            ammoInWeaponUI.text = "";
            ammoInBagUI.text = "";
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