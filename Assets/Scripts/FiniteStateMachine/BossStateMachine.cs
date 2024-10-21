using FiniteStateMachine.States;
using UnityEngine;

namespace FiniteStateMachine
{
    [RequireComponent(typeof(Rigidbody))]
    public class BossStateMachine : StateMachine
    {
        [HideInInspector] public Patrolling patrollingState;
        [HideInInspector] public Pursuing pursuingState;
        [HideInInspector] public Attacking attackingState;

        public Transform[] waypoints;
        public Rigidbody rigidBody;

        public float speed;
        public float distanceToChangeWaypoint;
        public float distanceToChase;
        public float distanceToAttack;

        public Transform player;

        private void Awake()
        {
            if (!rigidBody) rigidBody = GetComponent<Rigidbody>();
            
            var myTransform = transform;
            patrollingState = new Patrolling(this, myTransform, rigidBody, speed, distanceToChangeWaypoint, distanceToChase, waypoints, player);
            pursuingState = new Pursuing(this, myTransform, player, rigidBody, speed, distanceToAttack);
            attackingState = new Attacking(this, myTransform, player, distanceToAttack);
        }

        protected override BaseState GetInitialState()
        {
            return patrollingState;
        }
    }
}