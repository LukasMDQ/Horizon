using System.Collections;
using UnityEngine;

namespace FiniteStateMachine
{
    public class SkeletonDistanceStateMachine : SkeletonStateMachine
    {
        [SerializeField] private float _attackCooldown = 3;
        [SerializeField] private GameObject _eBullet;
        [SerializeField] private Transform _spawnPoint;
        private bool _isAttacking;
        
        protected override void Awake()
        {
            skeletonAttackType = SkeletonAttackType.Distance;
            base.Awake();
        }

        public void Shoot()
        {
            _isAttacking = true;
            StartCoroutine(AutoFire());
        }

        public void StopAttacking()
        {
            _isAttacking = false;
        }

        private IEnumerator AutoFire()
        {
            while (_isAttacking)
            {
                var bullet = Instantiate(_eBullet, _spawnPoint.position, _spawnPoint.rotation);

                Destroy(bullet, 5f);
                yield return new WaitForSeconds(_attackCooldown);
            }
        }
    }
}