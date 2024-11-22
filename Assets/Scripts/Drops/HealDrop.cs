using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Lidia Paiva
// Lucas Hernandez
public class HealDrop : Drop
{
    public int healValue;
    public override void ApplyDropEffect(Collider other)
    {
        if (other.CompareTag("Player") && other.TryGetComponent(out Stats stats))
        {
            stats.Heal(healValue);
            base.ApplyDropEffect(other);
        }
    }
}
