using System;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

namespace FiniteStateMachine.States
{
    public class SkeletonPatrolling : BasePatrolling
    {
        private readonly SkeletonStateMachine _skeletonStateMachine;
        private readonly SkeletonStateMachine.SkeletonAttackType _skeletonAttackType;

        public SkeletonPatrolling(SkeletonStateMachine stateMachine, Transform myTransform, Transform player, NavMeshAgent agent, float speed, float distanceToChangeWaypoint, float distanceToChase, Transform[] waypoints, LayerMask layerMask, SkeletonStateMachine.SkeletonAttackType skeletonAttackType) : base(stateMachine, myTransform, player, agent, speed, distanceToChangeWaypoint, distanceToChase, waypoints, layerMask)
        {
            _skeletonStateMachine = stateMachine;
            _skeletonAttackType = skeletonAttackType;
        }
        
        public override void Enter()
        {
            if (_skeletonAttackType == SkeletonStateMachine.SkeletonAttackType.Distance)
            {
                _skeletonStateMachine.SetSkeletonAnimations(SkeletonStateMachine.SkeletonAnimationsType.Idle);
                return;
            }
            
            base.Enter();
            _skeletonStateMachine.SetSkeletonAnimations(SkeletonStateMachine.SkeletonAnimationsType.Patrolling);
        }

        public override void UpdatePhysics()
        {
            // ReSharper disable once Unity.PreferAddressByIdToGraphicsParams
            if (_skeletonStateMachine.animator.GetBool("deadM"))
            {
                stateMachine.ChangeState(_skeletonStateMachine.deathState);
                return;
            }

            switch (_skeletonAttackType)
            {
                case SkeletonStateMachine.SkeletonAttackType.Distance:
                    PursuitOrWaypoint(_skeletonStateMachine.pursuingState, true);
                    break;
                case SkeletonStateMachine.SkeletonAttackType.Melee:
                    PursuitOrWaypoint(_skeletonStateMachine.pursuingState);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}