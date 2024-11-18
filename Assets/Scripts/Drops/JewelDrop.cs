using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JewelDrop : Drop
{
    public int jewelValue;
    public ChaliceAnimation chalice;
    public override void ApplyDropEffect(Collider other)
    {
        Debug.Log($"ApplyDropEffect called. Object: {other.name}, Tag: {other.tag}");

        if (other.CompareTag("Player"))
        {
            Debug.Log("Player detected!");

            if (other.TryGetComponent(out Stats stats))
            {
                Debug.Log("Stats component found!");
                stats.AddJewel(jewelValue);
                chalice.JewelUpdate();
                base.ApplyDropEffect(other);
            }
            else
            {
                Debug.LogError("Stats component not found on Player!");
            }
        }
        else
        {
            Debug.LogWarning($"Object {other.name} does not have the 'Player' tag.");
        }
    }
}
