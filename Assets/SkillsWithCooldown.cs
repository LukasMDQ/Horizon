using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class SkillsWithCooldown : MonoBehaviour
{
    // Array de habilidades (GameObjects)
    public GameObject[] skills;

    // Tiempo de duraci�n activa de la habilidad (5 segundos)
    public float activeTime = 5f;

    // Tiempo de cooldown en segundos (10 segundos)
    public float cooldownTime = 10f;

    // Array para rastrear si las habilidades est�n en cooldown
    private bool[] onCooldown;
    [SerializeField] Slider _slider;
    void Start()
    {
        _slider = GetComponent<Slider>();
        // Inicializar el array de cooldown para cada habilidad
        onCooldown = new bool[skills.Length];

        // Desactivar todas las habilidades al inicio
        foreach (GameObject skill in skills)
        {
            skill.SetActive(false);
        }
    }

    void Update()
    {
        // Comprobar si las teclas se presionan y si las habilidades est�n fuera de cooldown
        if (Input.GetKeyDown(KeyCode.Alpha1) && !onCooldown[0])
        {
            ActivateSkill(0);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2) && !onCooldown[1])
        {
            ActivateSkill(1);
        }
        if (Input.GetKeyDown(KeyCode.Alpha3) && !onCooldown[2])
        {
            ActivateSkill(2);
        }
    }

    // M�todo para activar la habilidad correspondiente
    void ActivateSkill(int index)
    {
        // Asegurarse de que el �ndice es v�lido
        if (index >= 0 && index < skills.Length)
        {
            // Activar la habilidad seleccionada
            skills[index].SetActive(true);

            // Iniciar corrutina para desactivar la habilidad despu�s de 5 segundos
            StartCoroutine(DeactivateAfterTime(index));

            // Iniciar cooldown para esa habilidad
            StartCoroutine(Cooldown(index));
        }
    }

    // Corrutina para desactivar la habilidad despu�s de un tiempo
    IEnumerator DeactivateAfterTime(int index)
    {
        // Esperar el tiempo que la habilidad debe estar activa
        yield return new WaitForSeconds(activeTime);

        // Desactivar la habilidad
        skills[index].SetActive(false);
    }

    // Corrutina para gestionar el cooldown de las habilidades
    IEnumerator Cooldown(int index)
    {
        // Establecer la habilidad como en cooldown
        onCooldown[index] = true;

        // Esperar por el tiempo de cooldown
        yield return new WaitForSeconds(cooldownTime);

        // Habilidad disponible nuevamente
        onCooldown[index] = false;
    }
}
