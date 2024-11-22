using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// Lidia Paiva
// Lucas Hernandez
public class JewelDrop : Drop
{
    public int jewelValue;
    public ChaliceAnimation chalice;
    public override void ApplyDropEffect(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (other.TryGetComponent(out Stats stats))
            {
                stats.AddJewel(jewelValue);
                chalice.JewelUpdate();
                base.ApplyDropEffect(other);
            }
        }
    }
}
