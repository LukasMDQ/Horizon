using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class AnimationWeapons : MonoBehaviour
{
    public WeaponChanger changer; // Referencia a cambio de Armas
    public Stats stats; // Referencia a Stats para cantidad de munición

    // Animators de armas
    public Animator animatorPistol; 
    public Animator animatorSword; 
    public Animator animatorShotgun; 
    public Animator animatorSwordCol;

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && changer.selectedWeapon == 0)
        {
            animatorPistol.SetTrigger("PistolShoot");
        }
        else if (Input.GetMouseButtonDown(0) && changer.selectedWeapon == 2 && stats.stamina >= 25f)
        {
            animatorSword.SetTrigger("SwordShoot");
            animatorSwordCol.SetTrigger("CollisionTrigger");
        }
        else if (Input.GetMouseButtonDown(0) && changer.selectedWeapon == 1 && stats.bulletCount > 0)
        {
            animatorShotgun.SetTrigger("ShotgunShoot");
        }
    }
}
