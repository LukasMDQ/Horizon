using UnityEngine;

namespace Weapons
{
    public abstract class Weapon : MonoBehaviour
    {
        public int damage;
        public AnimationWeapons animationWeapons;
        
        public abstract void Attack();
    }
}
