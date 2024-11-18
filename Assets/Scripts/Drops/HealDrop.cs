using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealDrop : Drop
{
    public int healValue;
    public override void ApplyDropEffect(Collider other)
    {
        Debug.Log($"ApplyDropEffect called. Object: {other.name}, Tag: {other.tag}");
        if (other.CompareTag("Player") && other.TryGetComponent(out Stats stats))
        {
            stats.Heal(healValue);
            base.ApplyDropEffect(other);
        }
    }
}
