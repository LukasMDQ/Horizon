using UnityEngine;
using UnityEngine.AI;

namespace FiniteStateMachine.States
{
    public class SkeletonPursuing : BasePursuing
    {
        private readonly SkeletonStateMachine _skeletonStateMachine;
        private readonly SkeletonStateMachine.SkeletonAttackType _skeletonAttackType;
        
        public SkeletonPursuing(SkeletonStateMachine stateMachine, Transform myTransform, Transform player, NavMeshAgent agent, float speed, float distanceToAttack, LayerMask layerMask, SkeletonStateMachine.SkeletonAttackType skeletonAttackType) : base(stateMachine, myTransform, player, agent, speed, distanceToAttack, layerMask)
        {
            _skeletonStateMachine = stateMachine;
            _skeletonAttackType = skeletonAttackType;
        }

        public override void Enter()
        {
            var auxSpeed = speed * 2;
            agent.speed = auxSpeed;
            agent.acceleration = auxSpeed * 2;

            agent.isStopped = false;
            
            _skeletonStateMachine.SetSkeletonAnimations(SkeletonStateMachine.SkeletonAnimationsType.Pursuing);
        }

        public override void Exit()
        {
            agent.speed = speed;
            agent.acceleration = speed * 2;

            if (_skeletonAttackType == SkeletonStateMachine.SkeletonAttackType.Distance)
            {
                agent.isStopped = true;
            }
        }

        public override void UpdatePhysics()
        {
            // ReSharper disable once Unity.PreferAddressByIdToGraphicsParams
            if (_skeletonStateMachine.animator.GetBool("deadM"))
            {
                stateMachine.ChangeState(_skeletonStateMachine.deathState);
                return;
            }

            PursueOrAttack(_skeletonStateMachine.attackingState);
        }
    }
}