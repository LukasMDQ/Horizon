using UnityEngine;
using UnityEngine.AI;

namespace FiniteStateMachine.States
{
    public abstract class BasePursuing : BaseState
    {
        private readonly Transform _myTransform;
        private readonly Transform _player;
        protected readonly NavMeshAgent agent;

        protected readonly float speed;
        private readonly float _distanceToAttack;

        private readonly LayerMask _layerMask;

        protected BasePursuing(StateMachine stateMachine, Transform myTransform, Transform player, NavMeshAgent agent, float speed, float distanceToAttack, LayerMask layerMask) : base(stateMachine)
        {
            this.stateMachine = stateMachine;

            _myTransform = myTransform;
            _player = player;
            this.agent = agent;

            this.speed = speed;
            this.agent.speed = speed;
            this.agent.acceleration = speed * 2;
            
            _distanceToAttack = distanceToAttack;
            _layerMask = layerMask;
        }

        public override void UpdateLogic()
        {
            UpdatePlayerPosition();
        }

        public abstract override void UpdatePhysics();
        
        protected void PursueOrAttack(BaseAttacking baseAttackingState)
        {
            var myPosition = _myTransform.position;
            var ray = new Ray(myPosition, (_player.position - myPosition).normalized);

            if (!Physics.Raycast(ray, out var hit, _distanceToAttack, _layerMask, QueryTriggerInteraction.Ignore)) return;

            if (hit.transform.gameObject.CompareTag("Player"))
            {
                stateMachine.ChangeState(baseAttackingState);
            }
        }

        private void UpdatePlayerPosition()
        {
            var position = _myTransform.position;
            var vectorToPlayer = _player.position - position;
            
            agent.SetDestination(vectorToPlayer.normalized * (vectorToPlayer.magnitude - 1) + position);
        }
    }
}