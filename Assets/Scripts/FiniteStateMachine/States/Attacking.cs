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

        public override void UpdateLogic()
        {
            if ((_player.position - _myTransform.position).magnitude > _distanceToAttack)
            {
                stateMachine.ChangeState(((BossStateMachine) stateMachine).pursuingState);
            }
            else
            {
                if (!_isAttacking)
                {
                    _attackTimer += Time.deltaTime;
                    
                    if (_attackTimer < 2) return;

                    _attackTimer = 0;
                    _isAttacking = true;

                    _attackArea.enabled = true;
                }
                else
                {
                    _attackTimer += Time.deltaTime;
                    
                    if (_attackTimer < 1) return;

                    _attackArea.enabled = false;
                    
                    if (_attackTimer < 2) return;

                    _attackTimer = 0;
                    _isAttacking = false;
                }
            }
        }
    }
}