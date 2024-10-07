using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonMelee : SkeletonEnemy
{
    protected override void MyStart()
    {
        base.MyStart();
        attackRange = 2;
    }

    protected override void Attack()
    {
        if (!_isAttacking)
        {
            _isAttacking = true;
            _isChasing = false;
            _agent.isStopped = true;
            SetAnimationBooleans(false, false, true, false); // Activa la animación de ataque (attackM).
        }
    }
}