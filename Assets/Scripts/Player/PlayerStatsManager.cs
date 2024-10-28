using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PlayerStatsManager
{
    public static int HP { get; set; }
    public static int MaxHP { get; set; }

    public static bool isChargeChalice = true;
    [SerializeField]
    public static int chargeMaxUses = 5;
    public static int chargeActualUses = 5;

    public static int jewels;

}
