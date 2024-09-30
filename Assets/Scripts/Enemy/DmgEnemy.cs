using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// ReSharper disable InconsistentNaming

// ReSharper disable once CheckNamespace
public class DmgEnemy : MonoBehaviour
{
    [SerializeField] private int _damage;
    [SerializeField] private GameObject _impactEnemy;
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Stats stats) && other.CompareTag("Player"))  //Interactura con el enemigo y se destruye.
        {
            stats.TakeDamage(_damage);
            SlashHitSound();
        }        
    }

    private void SlashHitSound()
    {
        var myTransform = transform;
        Instantiate(_impactEnemy, myTransform.position, myTransform.rotation);
        //Destroy(_slash, 1f);
    }
}