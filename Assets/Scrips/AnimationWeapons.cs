using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class AnimationWeapons : MonoBehaviour
{
    public GameObject weaponPistol; //0
    public GameObject weaponSword; //2
    public GameObject weaponShotgun; //1
    public GameObject swordCollider; //1
    public WeaponChanger changer;
    public Stats stats;

    Animator animatorPistol; Animator animatorSword; Animator animatorShotgun; Animator animatorSwordCol;

    void Start()
    {
        animatorPistol = weaponPistol.GetComponent<Animator>();
        animatorSword = weaponSword.GetComponent<Animator>();
        animatorShotgun = weaponShotgun.GetComponent<Animator>();
        animatorSwordCol = swordCollider.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && changer.selectedWeapon == 0)
        {
            animatorPistol.SetTrigger("PistolShoot");
        }
        else if (Input.GetMouseButtonDown(0) && changer.selectedWeapon == 2)
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
