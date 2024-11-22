using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Rendering;

[RequireComponent(typeof(Animator))]

// TP - 2 - Luchetti, Nicolás - Ahumada, Leandro 
// Control de animaciones del Caliz.

public class ChaliceAnimation : MonoBehaviour
{
    private Animator _chalice;
    public Skills skills;
    public Stats stats;
    public GameObject jewel1;
    public GameObject jewel2;
    public GameObject jewel3;
    
    //Declaracion de Nombres de Triggers para facil modificación/acceso

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
        if (Input.GetKeyDown(KeyCode.Alpha1)) //Habilidad - chequea si está en cooldown y cuantas gemas hay, preparado para más adelante si tenemos 3 anim para cada habilidad
        {
            if(!skills.onCooldown[0] && PlayerStatsManager.jewels >= 1)
            {
                _chalice.SetTrigger(Trigger);
            }
            else
            {
                _chalice.SetTrigger(Cooldown); //Animación de que no se puede utilizar ahora.
            }
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            if (!skills.onCooldown[1] && PlayerStatsManager.jewels >= 2)
            {
                _chalice.SetTrigger(Trigger); //"Trigger" cambiará cuando tengamos distintas animaciones, por ahora utilizan la misma.
            }
            else
            {
                _chalice.SetTrigger(Cooldown);
            }
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            if (!skills.onCooldown[2] && PlayerStatsManager.jewels >= 3)
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
        switch (PlayerStatsManager.jewels) //Setear la visibilidad dependiendo de cuantas gemas tenemos.
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

    public void FountainUse() //Trigger de Animación de un Caliz colocado en una fuente.
    {
        _chalice.SetTrigger("Fountain");
    }
}