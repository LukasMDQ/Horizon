using UnityEngine;

[RequireComponent(typeof(Animator))]
public abstract class SkeletonEnemy : Entity
{
    private Animator _animator; // There are still some things I need to work on
    private static readonly int Hit = Animator.StringToHash("hit");

    protected override void Start()
    {
        base.Start();
        if (!_animator) _animator = GetComponent<Animator>();
    }
    
    private void SetHitAnimation()
    {
        _animator.SetTrigger(Hit);
    }

    public override void Death() // TODO refactor this
    {
        base.Death();
        if (curHp <= 0)
        {
            var myTransform = transform;
            Instantiate(_destroyEffect, myTransform.position, myTransform.rotation);
            Destroy(gameObject);
        }
    }

    public override void TakeDamage(float damage)
    {
        SetHitAnimation();
        if (_animator.GetCurrentAnimatorStateInfo(0).IsName("Hit_Sword")) // TODO refactor this later
        {
            //_isReceivingDamage = true;
            //_agent.isStopped = true;
            //_agent.speed = 0f;
            //isChasing = false;
            //_isAttacking = false;
        }
        
        base.TakeDamage(damage);
    }
}