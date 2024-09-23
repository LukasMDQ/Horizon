using System.Collections;
using System.Collections.Generic;
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
        //Destroy(_slash, 1f);
    }
    void AirSlashSound()
    {
        Instantiate(_airslash, transform.position, transform.rotation);
        //Destroy(_airslash, 1f);
    }
}
