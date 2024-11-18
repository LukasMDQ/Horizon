using UnityEngine;

namespace FiniteStateMachine.States
{
    // ReSharper disable once UnusedType.Global
    public class SkeletonAttacking : BaseAttacking
    {
        private readonly SkeletonStateMachine _skeletonStateMachine;
        private readonly SkeletonStateMachine.SkeletonAttackType _skeletonAttackType;
        
        public SkeletonAttacking(SkeletonStateMachine stateMachine, Transform myTransform, Transform player, float distanceToAttack, SkeletonStateMachine.SkeletonAttackType skeletonAttackType) : base(stateMachine, myTransform, player, distanceToAttack)
        {
            _skeletonStateMachine = stateMachine;
            _skeletonAttackType = skeletonAttackType;
        }

        public override void Exit()
        {
            base.Exit();
            if (_skeletonAttackType == SkeletonStateMachine.SkeletonAttackType.Distance)
            {
                ((SkeletonDistanceStateMachine) _skeletonStateMachine).StopAttacking();
            }
        }

        public override void UpdateLogic()
        {
            if (_skeletonStateMachine.IsDead())
            {
                stateMachine.ChangeState(_skeletonStateMachine.deathState);
                return;
            }
            if ((player.position - myTransform.position).magnitude > distanceToAttack)
            {
                stateMachine.ChangeState(_skeletonStateMachine.pursuingState);
                return;
            }

            var auxVector3 = myTransform.forward;
            var direction = player.transform.position - myTransform.position;
            myTransform.forward = new Vector3(direction.x, auxVector3.y, direction.z);

            if (isAttacking) return;

            isAttacking = true;
            
            switch (_skeletonAttackType)
            {
                case SkeletonStateMachine.SkeletonAttackType.Melee:
                    _skeletonStateMachine.SetSkeletonAnimations(SkeletonStateMachine.SkeletonAnimationsType.AttackM);
                    break;
                case SkeletonStateMachine.SkeletonAttackType.Distance:
                    _skeletonStateMachine.SetSkeletonAnimations(SkeletonStateMachine.SkeletonAnimationsType.AttackD);
                    ((SkeletonDistanceStateMachine) _skeletonStateMachine).Shoot();
                    break;
                default:
                    Debug.LogError("Skeleton attack type not defined");
                    break;
            }
        }
    }
}