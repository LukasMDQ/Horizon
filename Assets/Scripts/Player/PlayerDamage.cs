using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDamage : MonoBehaviour
{
    public Stats stats;
    [SerializeField] private int dmg;
    [SerializeField] private GameObject _slash;
    [SerializeField] private GameObject _airslash;

    private void Awake()
    {
        dmg = stats.damage;
    }

    private void Update()
    {
        dmg = stats.damage;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Stats thisStats) && other.CompareTag("Enemy"))  //Interactua con el enemigo y se destruye.
        {
            thisStats.TakeDamage(dmg);
            SlashSound();
        }
        else
        {
            AirSlashSound();
        }

    }

    private void SlashSound()
    {
        var myTransform = transform;
        Instantiate(_slash, myTransform.position, myTransform.rotation);
        stats.stamina -= 50f; //Saca Stamina 

        StopCoroutine(RechargeStamina());

        StartCoroutine(RechargeStamina());

        /*if (stats.stamina >= stats.maxStamina)
        {
            StopCoroutine(RechargeStamina()); //Para la Corutina cuando la stamina se llena
        }
        else
        {
            StartCoroutine(RechargeStamina()); //Inicia la Corutina si es menor a la Stamina Maxima
        }*/
        //Destroy(_slash, 1f);
    }

    private void AirSlashSound()
    {
        var myTransform = transform;
        Instantiate(_airslash, myTransform.position, myTransform.rotation);
        stats.stamina -= 25f; //Saca Stamina 

        StopCoroutine(RechargeStamina());

        StartCoroutine(RechargeStamina());

        /*if (stats.stamina >= stats.maxStamina)
        {
            StopCoroutine(RechargeStamina()); //Para la Corutina cuando la stamina se llena
        }
        else
        {
            StartCoroutine(RechargeStamina()); //Inicia la Corutina si es menor a la Stamina Maxima
        }*/

        //Destroy(_airslash, 1f);
    }
    private IEnumerator RechargeStamina() //Recarga de Stamina
    {
        yield return new WaitForSeconds(2f);

        while (stats.stamina < stats.maxStamina)
        {
            stats.stamina += stats.chargeRate / 10f; //Recharge Rate para controlar qué tan rapido se regenera

            if (stats.stamina > stats.maxStamina)
            {
                stats.stamina = stats.maxStamina; // Cap de Stamina
            }
            yield return new WaitForSeconds(0.1f);
        }
    }
}