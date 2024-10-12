using UnityEngine;

namespace Weapons
{
    public class Sword : Weapon
    {
        public override void Attack()
        {
            if (true) // TODO change this for stamina check
            {
                animationWeapons.AnimateThisMelee(this);
            }
        }
    }
}