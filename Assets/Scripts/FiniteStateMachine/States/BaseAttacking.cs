using UnityEngine;

namespace FiniteStateMachine.States
{
    public class BaseAttacking : BaseState
    {
        protected readonly Transform myTransform;
        protected readonly Transform player;
        protected readonly float distanceToAttack;

        protected bool isAttacking;

        protected BaseAttacking(StateMachine stateMachine, Transform myTransform, Transform player, float distanceToAttack) : base(stateMachine)
        {
            this.stateMachine = stateMachine;

            this.myTransform = myTransform;
            this.player = player;
            this.distanceToAttack = distanceToAttack;
        }

        public override void Exit()
        {
            isAttacking = false;
        }

        public void ResetAttack()
        {
            isAttacking = false;
        }
    }
}