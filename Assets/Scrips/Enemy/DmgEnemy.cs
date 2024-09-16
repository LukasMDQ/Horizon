using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DmgEnemy : MonoBehaviour
{
    [SerializeField] int _damage;   
    
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Stats stats) && other.CompareTag("Player"))  //Interactura con el enemigo y se destruye.
        {
            stats.TakeDamage(_damage);           
        }        
    }
}
