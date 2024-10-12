using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using Weapons;

public class AnimationWeapons : MonoBehaviour
{
    public WeaponChanger changer; // Referencia a cambio de Armas
    public Stats stats; // Referencia a Stats para cantidad de munición

    // Animators de armas
    public Animator animatorPistol; 
    public Animator animatorSword; 
    public Animator animatorShotgun; 
    public Animator animatorSwordCol;
    
    private static readonly int PistolShoot      = Animator.StringToHash("PistolShoot");
    private static readonly int ShotgunShoot     = Animator.StringToHash("ShotgunShoot");
    private static readonly int SwordShoot       = Animator.StringToHash("SwordShoot");
    private static readonly int CollisionTrigger = Animator.StringToHash("CollisionTrigger");

    /*void Update()
    {
        if (Input.GetMouseButtonDown(0) && changer.selectedWeapon == 0)
        {
            animatorPistol.SetTrigger("PistolShoot");
        }
        else if (Input.GetMouseButtonDown(0) && changer.selectedWeapon == 2 && stats.stamina >= 25f) // TODO make it so that the player can only attack if he has enough stamina
        {
            animatorSword.SetTrigger("SwordShoot");
            animatorSwordCol.SetTrigger("CollisionTrigger");
        }
        else if (Input.GetMouseButtonDown(0) && changer.selectedWeapon == 1 && stats.bulletCount > 0)
        {
            animatorShotgun.SetTrigger("ShotgunShoot");
        }
    }*/

    public void AnimateThisRanged(RangedWeapon weaponToAnimate)
    {
        weaponToAnimate.TryGetComponent(out Pistol pistol);
        weaponToAnimate.TryGetComponent(out Rifle rifle);

        if (pistol)
        {
            animatorPistol.SetTrigger(PistolShoot);
        }
        else if (rifle)
        {
            animatorShotgun.SetTrigger(ShotgunShoot);
        }
    }

    public void AnimateThisMelee(Sword sword)
    {
        if (sword)
        {
            animatorSword.SetTrigger(SwordShoot);
            animatorSwordCol.SetTrigger(CollisionTrigger);
        }
    }
}