using System.Collections;
using UnityEngine;
using UnityEngine.UI;  // Necesario para usar UI como Slider

public class Skills : MonoBehaviour
{
    public GameObject[] skillsObject;
    private Stats _stats;

    public Slider[] cooldownSliders;

    //  duración activa de la habilidad (5 seg)
    public float activeTime = 5f;

    //  cooldown en segundos (10 seg)
    public float cooldownTime = 10f;

    // habilidades  en cooldown
    private bool[] _onCooldown;

    // tiempo restante de cooldown de cada habilidad
    private float[] _cooldownTimers;

    // ReSharper disable once InconsistentNaming
    [SerializeField] private SkillsCooldownFeedback[] _skillsCooldownFeedback;

    private void Start()
    {
        _onCooldown = new bool[skillsObject.Length];
        _cooldownTimers = new float[skillsObject.Length];
        _stats = GetComponent<Stats>();
        
        foreach (GameObject skill in skillsObject)
        {
            skill.SetActive(false);
        }

        for (int i = 0; i < cooldownSliders.Length; i++)
        {
            cooldownSliders[i].value = 1f;  // 1 significa que no está en cooldown
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1) && !_onCooldown[0] && _stats.jewels >= 1)
        {
            ActivateSkill(0);
            _stats.Heal(10);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2) && !_onCooldown[1] && _stats.jewels >= 2)
        {
            ActivateSkill(1);
            _stats.Buff(10);
        }
        if (Input.GetKeyDown(KeyCode.Alpha3) && !_onCooldown[2] && _stats.jewels >= 3)
        {
            ActivateSkill(2);
        }

        // Actualiza tiempos de cooldown
        for (int i = 0; i < skillsObject.Length; i++)
        {
            if (_onCooldown[i])
            {
                _cooldownTimers[i] -= Time.deltaTime;
                cooldownSliders[i].value = _cooldownTimers[i] / cooldownTime;  // Actualiza el valor del slider
            }
            else
            {
                cooldownSliders[i].value = 1f;  // Resetea el slider 
            }
        }
    }

    private void ActivateSkill(int index)
    {
        if (index >= 0 && index < skillsObject.Length)
        {
            // Activar la habilidad 
            skillsObject[index].SetActive(true);
            
            // Start the feedback
            StartCoroutine(_skillsCooldownFeedback[index].ActivateCooldown(cooldownTime));

            // desactivar la habilidad 
            StartCoroutine(DeactivateAfterTime(index));

            // Iniciar cooldown para esa habilidad
            StartCoroutine(Cooldown(index));
        }
    }

    //  desactivar la habilidad después de un tiempo
    private IEnumerator DeactivateAfterTime(int index)
    {
        //  tiempo que la habilidad debe estar activa
        yield return new WaitForSeconds(activeTime);

        // Desactivar la habilidad
        skillsObject[index].SetActive(false);
    }

    // cooldown de las habilidades
    private IEnumerator Cooldown(int index)
    {
        // Establecer la habilidad en cooldown y tiempo restante
        _onCooldown[index] = true;
        _cooldownTimers[index] = cooldownTime;

        yield return new WaitForSeconds(cooldownTime);

        // Habilidad disponible 
        _onCooldown[index] = false;
    }
}
