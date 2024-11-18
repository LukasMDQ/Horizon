using UnityEngine;
using UnityEngine.AI;

namespace FiniteStateMachine.States
{
    public class BossPatrolling : BasePatrolling
    {
        private readonly BossStateMachine _bossStateMachine;
        
        public BossPatrolling(BossStateMachine stateMachine, Transform myTransform, Transform player, NavMeshAgent agent, float speed, float distanceToChangeWaypoint, float distanceToChase, Transform[] waypoints, LayerMask layerMask) : base(stateMachine, myTransform, player, agent, speed, distanceToChangeWaypoint, distanceToChase, waypoints, layerMask)
        {
            _bossStateMachine = stateMachine;
        }

        public override void Enter()
        {
            base.Enter();
            _bossStateMachine.SetBossAnimations(BossStateMachine.BossAnimationsType.Patrolling);
        }

        public override void UpdatePhysics()
        {
            // ReSharper disable once Unity.PreferAddressByIdToGraphicsParams
            if (_bossStateMachine.animator.GetBool("Dead"))
            {
                stateMachine.ChangeState(_bossStateMachine.deathState);
                return;
            }
            
            PursuitOrWaypoint(_bossStateMachine.pursuingState);
        }
    }
}