using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PlayerStatsManager
{
    public static int HP { get; set; } = 100;
    public static int MaxHP { get; set; } = 100;

    public static bool isChargeChalice = true;
    [SerializeField]
    public static int chargeMaxUses = 5;
    public static int chargeActualUses = 5;

    public static int jewels;

    public static bool isInvulnerable = false;

    public static int easterEggAcc = 3;
}
