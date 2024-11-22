using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// TP 2 - Ahumada, Leandro
// Script de Daño a Meleé.

public class PlayerDamage : MonoBehaviour
{
    public Stats stats;
    [SerializeField] private int dmg;
    [SerializeField] private GameObject _slash;
    [SerializeField] private GameObject _airslash;
    [SerializeField] float _staminaCost;

    private void Awake() //Set Daño
    {
        dmg = stats.damage;
    }

    private void Update() //Consumo de Stamina.
    {
        if (Input.GetKey (KeyCode.LeftShift)) //Detiene cualquier recarga de energia y se consume.
        {
            StopAllCoroutines();
            stats.stamina -= _staminaCost* Time.deltaTime;
        }
        else if (Input.GetKeyUp (KeyCode.LeftShift))
        {
            if (stats.stamina < stats.maxStamina) //Al dejar de Sprintar, regenera Stamina.
            {
                StartCoroutine(RechargeStamina());
            }
        }
        
       
        dmg = stats.damage;
    }
    private void OnTriggerEnter(Collider other) //Gasta Stamina y reproduce un sonido distinto dependiendo si golpea un enemigo o no.
    {
        if (other.TryGetComponent(out Stats thisStats) && other.CompareTag("Enemy"))  // Interactua con el enemigo y se destruye.
        {
            thisStats.TakeDamage(dmg);
            SlashSound();
        }
        else
        {
            AirSlashSound();
        }
    }

    private void SlashSound() //Caso de Golpear Enemigo
    {
        if (stats.stamina >= 25)
        {
            var myTransform = transform;
            Instantiate(_slash, myTransform.position, myTransform.rotation);
            stats.stamina -= 25f; //Saca Stamina 

            // StopCoroutine(RechargeStamina());
            StopAllCoroutines();
            StartCoroutine(RechargeStamina());
        }
    }
    private void AirSlashSound() //Caso de Golpear al Aire
    {
        if (stats.stamina >= 25)
        {
            var myTransform = transform;
            Instantiate(_airslash, myTransform.position, myTransform.rotation);
            stats.stamina -= 25f; //Saca Stamina 

            //StopCoroutine(RechargeStamina());
            StopAllCoroutines();
            StartCoroutine(RechargeStamina());
        }
    }
    private IEnumerator RechargeStamina() //Recarga de Stamina
    {
        yield return new WaitForSeconds(0.5f); //Cooldown antes de regenerar

        while (stats.stamina < stats.maxStamina)
        {
            stats.stamina += stats.chargeRate / 10f; //Recharge Rate para controlar qué tan rapido se regenera

            if (stats.stamina > stats.maxStamina)
            {
                stats.stamina = stats.maxStamina; //Clamp de Stamina
            }
            yield return new WaitForSeconds(0.1f);
        }
    }
}