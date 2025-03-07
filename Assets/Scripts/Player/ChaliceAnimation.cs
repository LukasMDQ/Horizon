using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Rendering;

[RequireComponent(typeof(Animator))]

//TP FINAL- - Luchetti, Nicolás - Ahumada, Leandro 
// Control de animaciones del Caliz.

public class ChaliceAnimation : MonoBehaviour
{
    private Animator _chalice;
    public Skills skills;
    public Stats stats;
    public GameObject[] jewels;  


    //Declaracion de Nombres de Triggers para facil modificación/acceso

    private static readonly int Trigger = Animator.StringToHash("Trigger");
    private static readonly int Cooldown = Animator.StringToHash("Cooldown");    
    private static int[] JewelsHash = new int[3];

    private void Start()
    {
        _chalice = GetComponent<Animator>();

        JewelsHash[0] = Animator.StringToHash("Jewel1");
        JewelsHash[2] = Animator.StringToHash("Jewel2");
        JewelsHash[1] = Animator.StringToHash("Jewel3");
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
                jewels[0].SetActive(true);
                _chalice.SetTrigger(JewelsHash[0]);
                break;
            case 2:
                jewels[1].SetActive(true);
                _chalice.SetTrigger(JewelsHash[1]);
                break;
            case 3:
                jewels[2].SetActive(true);
                _chalice.SetTrigger(JewelsHash[2]);
                break;
        }
    }

    public void FountainUse() //Trigger de Animación de un Caliz colocado en una fuente.
    {
        _chalice.SetTrigger("Fountain");
    }
}