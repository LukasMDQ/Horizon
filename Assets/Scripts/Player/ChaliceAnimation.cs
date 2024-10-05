using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

// ReSharper disable once CheckNamespace
[RequireComponent(typeof(Animator))]
public class ChaliceAnimation : MonoBehaviour
{
    private Animator _chalice;
    public Skills skills;
    public Stats stats;
    public GameObject jewel1;
    public GameObject jewel2;
    public GameObject jewel3;
    
    private static readonly int Trigger = Animator.StringToHash("Trigger");
    private static readonly int Cooldown = Animator.StringToHash("Cooldown");
    private static readonly int Jewel1 = Animator.StringToHash("Jewel1");
    private static readonly int Jewel2 = Animator.StringToHash("Jewel2");
    private static readonly int Jewel3 = Animator.StringToHash("Jewel3");

    private void Start()
    {
        _chalice = GetComponent<Animator>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1)) //Habilidad 1 - chequea si está en cooldown y cuantas gemas hay, preparado para más adelante si tenemos 3 anim para cada habilidad
        {
            if(!skills.onCooldown[0] && stats.jewels >= 1)
            {
                _chalice.SetTrigger(Trigger);
            }
            else
            {
                _chalice.SetTrigger(Cooldown);
            }
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            if (!skills.onCooldown[1] && stats.jewels >= 2)
            {
                _chalice.SetTrigger(Trigger);
            }
            else
            {
                _chalice.SetTrigger(Cooldown);
            }
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            if (!skills.onCooldown[2] && stats.jewels >= 3)
            {
                _chalice.SetTrigger(Trigger);
            }
            else
            {
                _chalice.SetTrigger(Cooldown);
            }
        }
    }

    public void JewelUpdate()
    {
        switch (stats.jewels) //Setear la visibilidad dependiendo de cuantas gemas tenemos
        {
            case 1:
                jewel1.SetActive(true);
                _chalice.SetTrigger(Jewel1);
                break;
            case 2:
                jewel2.SetActive(true);
                _chalice.SetTrigger(Jewel2);
                break;
            case 3:
                jewel3.SetActive(true);
                _chalice.SetTrigger(Jewel3);
                break;
        }
    }
}