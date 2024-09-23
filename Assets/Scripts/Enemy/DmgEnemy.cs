using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DmgEnemy : MonoBehaviour
{
    [SerializeField] int _damage;
    [SerializeField] private GameObject _impactEnemy;

    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Stats stats) && other.CompareTag("Player"))  //Interactura con el enemigo y se destruye.
        {
            stats.TakeDamage(_damage);
            SlashHitSound();
        }        
    }
    void SlashHitSound()
    {
        Instantiate(_impactEnemy, transform.position, transform.rotation);
        //Destroy(_slash, 1f);
    }
}
