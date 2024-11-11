using UnityEngine;

namespace Weapons
{
    public class Sword : Weapon
       
    {
        public Stats stats;
        public override void Attack()
        {
            if (stats.stamina >= 25) // TODO change this for stamina check
            {
                animationWeapons.AnimateThisMelee(this);
            }
        }
    }
}