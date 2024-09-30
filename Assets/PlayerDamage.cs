using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

public class PlayerDamage : MonoBehaviour
{
    public Stats stats;
    [SerializeField] int dmg;
    [SerializeField] private GameObject _slash;
    [SerializeField] private GameObject _airslash;

    private void Awake()
    {
        dmg = stats.damage;
    }

    void Update()
    {
        dmg = stats.damage;
    }
    private void OnTriggerEnter(Collider other)
    {
        
        if (other.TryGetComponent(out Stats stats) && other.CompareTag("Enemy"))  //Interactura con el enemigo y se destruye.
        {
            stats.TakeDamage(dmg);
            SlashSound();
        }
        else
        {
            AirSlashSound();
        }

    }

    void SlashSound()
    {
        Instantiate(_slash, transform.position, transform.rotation);
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
    void AirSlashSound()
    {
        Instantiate(_airslash, transform.position, transform.rotation);
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
            stats.stamina += stats.ChargeRate / 10f; //Recharge Rate para controlar qué tan rapido se regenera

            if (stats.stamina > stats.maxStamina)
            {
                stats.stamina = stats.maxStamina; // Cap de Stamina
            }
            yield return new WaitForSeconds(0.1f);
        }
    }
}
