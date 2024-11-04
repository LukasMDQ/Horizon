using UnityEngine;

namespace FiniteStateMachine.States
{
    public class Attacking : BaseState
    {
        private readonly Transform _myTransform;
        private readonly Transform _player;
        private readonly float _distanceToAttack;
        private readonly Collider _attackArea;
        
        private bool _isAttacking;
        private float _attackTimer;

        public Attacking(StateMachine stateMachine, Transform myTransform, Transform player, float distanceToAttack, Collider attackArea) : base(stateMachine)
        {
            this.stateMachine = stateMachine;

            _myTransform = myTransform;
            _player = player;
            _distanceToAttack = distanceToAttack;

            _attackArea = attackArea;
        }

        public override void Exit()
        {
            _attackArea.enabled = false;
            _isAttacking = false;
            _attackTimer = 0;
        }

        public override void UpdateLogic()
        {
            if (((BossStateMachine) stateMachine).animator.GetBool("Dead"))
            {
                stateMachine.ChangeState(((BossStateMachine) stateMachine).deathState);
                return;
            }
            if ((_player.position - _myTransform.position).magnitude > _distanceToAttack)
            {
                stateMachine.ChangeState(((BossStateMachine) stateMachine).pursuingState);
                return;
            }
            
            
            if (!_isAttacking) // attack animation last 3.292f seconds
            {
                ((BossStateMachine) stateMachine).SetBossAnimations(BossStateMachine.BossAnimationsType.Attack);
                _isAttacking = true;
                
                _attackTimer += Time.deltaTime;
            }
            else
            {
                _attackTimer += Time.deltaTime;
                
                if (_attackTimer < 2) return;

                _attackArea.enabled = true;
                
                if (_attackTimer < 2.5f) return;
                
                _attackArea.enabled = false;
                
                if (_attackTimer < 3.292f) return;

                _attackTimer = 0;
                _isAttacking = false;
            }
        }
    }
}