using UnityEngine;

namespace FiniteStateMachine.States
{
    public class BossAttacking : BaseAttacking
    {
        private readonly BossStateMachine _bossStateMachine;
        
        public BossAttacking(BossStateMachine stateMachine, Transform myTransform, Transform player, float distanceToAttack) : base(stateMachine, myTransform, player, distanceToAttack)
        {
            _bossStateMachine = stateMachine;
        }

        public override void UpdateLogic()
        {
            if (_bossStateMachine.IsDead())
            {
                stateMachine.ChangeState(_bossStateMachine.deathState);
                return;
            }
            if ((player.position - myTransform.position).magnitude > distanceToAttack)
            {
                stateMachine.ChangeState(_bossStateMachine.pursuingState);
                return;
            }

            if (isAttacking) return;
            _bossStateMachine.SetBossAnimations(BossStateMachine.BossAnimationsType.Attack);
            isAttacking = true;
        }
    }
}