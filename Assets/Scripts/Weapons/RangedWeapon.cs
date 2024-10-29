using TMPro;
using UnityEngine;

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

        private float _reloadHudAlpha;

        protected virtual void Awake()
        {
            ammo = maxAmmo;
        }

        private void OnEnable()
        {
            UpdateUI();
            reloadUI.alpha = _reloadHudAlpha;
        }

        private void OnDisable()
        {
            ClearUI();
            reloadUI.alpha = 0f;
        }

        public override void Attack()
        {
            if (isAttacking) return;

            isAttacking = true;
            
            if (ammo > 0)
            {
                ammo--;
                UpdateUI();
                if (ammo <= 0)
                {
                    _reloadHudAlpha = 1f;
                    reloadUI.alpha = _reloadHudAlpha;
                }
                
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
        
        protected void UpdateUIReload()
        {
            _reloadHudAlpha = 0f;
            reloadUI.alpha = _reloadHudAlpha;
        }
    }
}