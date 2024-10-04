using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class CalizAnimation : MonoBehaviour
{
    Animator caliz;
    public Skills skills;
    public Stats stats;
    public GameObject jewel1;
    public GameObject jewel2;
    public GameObject jewel3;

    void Start()
    {
        caliz = GetComponent<Animator>();
    }

    void Update()
    {
        if(stats.jewels >= 1) //Setear la visibilidad dependiendo de cuantas gemas tenemos
        {
            jewel1.SetActive(true);
        }
        if (stats.jewels >= 2)
        {
            jewel2.SetActive(true);
        }
        if (stats.jewels >= 3)
        {
            jewel3.SetActive(true);
        }

        if (Input.GetKeyDown(KeyCode.Alpha1)){ //Habilidad 1 - chequea si está en cooldown y cuantas gemas hay, preparado para más adelante si tenemos 3 anim para cada habilidad
            if(!skills.onCooldown[0] && stats.jewels >= 1) {
                caliz.SetTrigger("Trigger");
            }
            else{
                caliz.SetTrigger("Cooldown");
            }
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            if (!skills.onCooldown[1] && stats.jewels >= 2)
            {
                caliz.SetTrigger("Trigger");
            }
            else
            {
                caliz.SetTrigger("Cooldown");
            }
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            if (!skills.onCooldown[2] && stats.jewels >= 3)
            {
                caliz.SetTrigger("Trigger");
            }
            else
            {
                caliz.SetTrigger("Cooldown");
            }
        }
    }
}
