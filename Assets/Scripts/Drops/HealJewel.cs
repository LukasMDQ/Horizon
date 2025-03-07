using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealJewel : Jewels
{
    [SerializeField] private Stats stats;

    public override void UseSkill(int value)
    {
        stats.Heal(value);
    }
}
