using UnityEngine;
using UnityEngine.AI;

namespace FiniteStateMachine.States
{
    public class Pursuing : BaseState
    {
        private readonly Transform _myTransform;
        private readonly Transform _player;
        private readonly NavMeshAgent _agent;
        
        private readonly float _speed;
        private readonly float _speedMultiplier;
        private readonly float _distanceToAttack;
        private float _distanceToChase;

        private float _timerToChange;

        private readonly LayerMask _layerMask;

        // ReSharper disable once SuggestBaseTypeForParameter
        public Pursuing(BossStateMachine stateMachine, Transform myTransform, Transform player, NavMeshAgent agent, float speed, float speedMultiplier, float distanceToAttack, float distanceToChase, LayerMask layerMask) : base(stateMachine)
        {
            this.stateMachine = stateMachine;

            _myTransform = myTransform;
            _player = player;
            _agent = agent;

            _speed = speed;
            _speedMultiplier = speedMultiplier;
            _distanceToAttack = distanceToAttack;
            _distanceToChase = distanceToChase;
            _layerMask = layerMask;
        }

        public override void Enter()
        {
            var auxSpeed = _speed * _speedMultiplier;
            _agent.speed = auxSpeed;
            _agent.acceleration = auxSpeed * 2;
            
            ((BossStateMachine) stateMachine).SetBossAnimations(BossStateMachine.BossAnimationsType.Pursuing);
        }

        public override void Exit()
        {
            var auxSpeed = _speed / _speedMultiplier;
            _agent.speed = auxSpeed;
            _agent.acceleration = auxSpeed * 2;
        }

        public override void UpdateLogic()
        {
            UpdatePlayerPosition();
        }

        public override void UpdatePhysics()
        {
            if (((BossStateMachine) stateMachine).animator.GetBool("Dead"))
            {
                stateMachine.ChangeState(((BossStateMachine) stateMachine).deathState);
                return;
            }
            var myPosition = _myTransform.position;
            var ray = new Ray(myPosition, (_player.position - myPosition).normalized);

            if (!Physics.Raycast(ray, out var hit, _distanceToAttack, _layerMask, QueryTriggerInteraction.Ignore)) return;
            
            if (hit.transform.gameObject.CompareTag("Player"))
            {
                stateMachine.ChangeState(((BossStateMachine) stateMachine).attackingState);
            }
        }
        
        private void UpdatePlayerPosition()
        {
            var position = _myTransform.position;
            var vectorToPlayer = _player.position - position;
            
            _agent.SetDestination(vectorToPlayer.normalized * (vectorToPlayer.magnitude - 1) + position);
        }
    }
}