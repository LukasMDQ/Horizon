using System;
using UnityEngine;

namespace Weapons
{
    public abstract class Weapon : MonoBehaviour
    {
        public int damage;
        public AnimationWeapons animationWeapons;
        protected bool isAttacking;
        
        public abstract void Attack();

        public bool IsAttacking => isAttacking;

        private void Update()
        {
            print(IsAttacking);
        }
    }
}