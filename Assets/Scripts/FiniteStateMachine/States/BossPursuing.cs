using UnityEngine;
using UnityEngine.AI;

namespace FiniteStateMachine.States
{
    public class BossPursuing : BasePursuing
    {
        private readonly BossStateMachine _bossStateMachine;
        private readonly float _speedMultiplier;
        
        public BossPursuing(BossStateMachine stateMachine, Transform myTransform, Transform player, NavMeshAgent agent, float speed, float speedMultiplier, float distanceToAttack, LayerMask layerMask) : base(stateMachine, myTransform, player, agent, speed, distanceToAttack, layerMask)
        {
            _bossStateMachine = stateMachine;
            _speedMultiplier = speedMultiplier;
        }

        public override void Enter()
        {
            var auxSpeed = speed * _speedMultiplier;
            agent.speed = auxSpeed;
            agent.acceleration = auxSpeed * 2;
            
            _bossStateMachine.SetBossAnimations(BossStateMachine.BossAnimationsType.Pursuing);
        }

        public override void Exit()
        {
            agent.speed = speed;
            agent.acceleration = speed * 2;
        }

        public override void UpdatePhysics()
        {
            // ReSharper disable once Unity.PreferAddressByIdToGraphicsParams
            if (_bossStateMachine.animator.GetBool("Dead"))
            {
                stateMachine.ChangeState(_bossStateMachine.deathState);
                return;
            }
            
            PursueOrAttack(_bossStateMachine.attackingState);
        }
    }
}