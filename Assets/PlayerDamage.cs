using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDamage : MonoBehaviour
{
    public Stats stats;
    [SerializeField] int dmg;
    private void Awake()
    {
        dmg = stats.damage;
    }

    // Update is called once per frame
    void Update()
    {
        dmg = stats.damage;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Stats stats) && other.CompareTag("Enemy"))  //Interactura con el enemigo y se destruye.
        {
            stats.TakeDamage(dmg);
        }
    }
}
