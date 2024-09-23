using System.Collections;
using UnityEngine;
using UnityEngine.UI;  // Necesario para usar UI como Slider

public class skills : MonoBehaviour
{

    public GameObject[] skillsObject;
    Stats stats;

    
    public Slider[] cooldownSliders;

    //  duración activa de la habilidad (5 seg)
    public float activeTime = 5f;

    //  cooldown en segundos (10 seg)
    public float cooldownTime = 10f;

    // habilidades  en cooldown
    private bool[] onCooldown;

    // tiempo restante de cooldown de cada habilidad
    private float[] cooldownTimers;

    void Start()
    {
        onCooldown = new bool[skillsObject.Length];
        cooldownTimers = new float[skillsObject.Length];
        stats = GetComponent<Stats>();


        foreach (GameObject skill in skillsObject)
        {
            skill.SetActive(false);
        }

        
        for (int i = 0; i < cooldownSliders.Length; i++)
        {
            cooldownSliders[i].value = 1f;  // 1 significa que no está en cooldown
        }
    }

    void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.Alpha1) && !onCooldown[0]&& stats.jewels >= 1)
        {
            ActivateSkill(0);
            stats.Heal(10);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2) && !onCooldown[1] && stats.jewels >= 2)
        {
            ActivateSkill(1);
            stats.Buff(10);
        }
        if (Input.GetKeyDown(KeyCode.Alpha3) && !onCooldown[2] && stats.jewels >= 3)
        {
            ActivateSkill(2);
        }

        // Actualiza tiempos de cooldown 
        for (int i = 0; i < skillsObject.Length; i++)
        {
            if (onCooldown[i])
            {
                cooldownTimers[i] -= Time.deltaTime;
                cooldownSliders[i].value = cooldownTimers[i] / cooldownTime;  // Actualiza el valor del slider
            }
            else
            {
                cooldownSliders[i].value = 1f;  // Resetea el slider 
            }
        }
    }

    
    void ActivateSkill(int index)
    {
        
        if (index >= 0 && index < skillsObject.Length)
        {
            // Activar la habilidad 
            skillsObject[index].SetActive(true);

            // desactivar la habilidad 
            StartCoroutine(DeactivateAfterTime(index));

            // Iniciar cooldown para esa habilidad
            StartCoroutine(Cooldown(index));
        }
    }

    //  desactivar la habilidad después de un tiempo
    IEnumerator DeactivateAfterTime(int index)
    {
        //  tiempo que la habilidad debe estar activa
        yield return new WaitForSeconds(activeTime);

        // Desactivar la habilidad
        skillsObject[index].SetActive(false);
    }

    // cooldown de las habilidades
    IEnumerator Cooldown(int index)
    {
        // Establecer la habilidad en cooldown y tiempo restante
        onCooldown[index] = true;
        cooldownTimers[index] = cooldownTime;

       
        yield return new WaitForSeconds(cooldownTime);

        // Habilidad disponible 
        onCooldown[index] = false;
    }
}

