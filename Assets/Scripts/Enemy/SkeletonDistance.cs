using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonDistance : SkeletonEnemy
{
    [SerializeField] private float _attackCooldown = 2;
    [SerializeField] private GameObject _eBullet;
    [SerializeField] private Transform _spawnPoint;
    private bool _isInCooldown;

    /*protected override void MyStart()
    {
        base.MyStart();
       // attackRange = 6;
    }*/

    protected override void Attack()
    {
        if (!_isAttacking)
        {
            _isAttacking = true;
            _isChasing = false;
            _agent.isStopped = true;
            StartCoroutine(AutoFire());
            SetAnimationBooleans(false, false, false, true, false); Debug.Log("attackdistance");
        }
    }

    private IEnumerator AutoFire()
    {
        if (!_isInCooldown)
        {
            _isInCooldown = true;

            while (_isAttacking)
            {
                GameObject bullet = Instantiate(_eBullet, _spawnPoint.position, _spawnPoint.rotation);

                Destroy(bullet, 5f);
                yield return new WaitForSeconds(_attackCooldown);
            }

            _isInCooldown = false;
        }
    }
}