using System;
using FiniteStateMachine.States;
using UnityEngine;
using UnityEngine.AI;

namespace FiniteStateMachine
{
    [RequireComponent(typeof(NavMeshAgent))]
    public class BossStateMachine : StateMachine
    {
        [HideInInspector] public Patrolling patrollingState;
        [HideInInspector] public Pursuing pursuingState;
        [HideInInspector] public Attacking attackingState;

        public Transform[] waypoints;
        public NavMeshAgent agent;

        public float speed;
        [Tooltip("The speed multiplier when chasing")] [Range(1, 2)]
        public float speedMultiplier = 1.5f;
        [Tooltip("Tolerance to change waypoint")] [Range(0f, 1.5f)]
        public float distanceToChangeWaypoint;
        public float distanceToChase;
        public float distanceToAttack;

        public Transform player;

        [Tooltip("Layers I can see")]
        public LayerMask layerMask;

        private void Awake()
        {
            var myTransform = transform;
            patrollingState = new Patrolling(this, myTransform, player, agent, speed, distanceToChangeWaypoint, distanceToChase, waypoints, layerMask);
            pursuingState = new Pursuing(this, myTransform, player, agent, speed, speedMultiplier, distanceToAttack, distanceToChase, layerMask);
            attackingState = new Attacking(this, myTransform, player, distanceToAttack);
        }

        protected override BaseState GetInitialState()
        {
            return patrollingState;
        }

        private void OnDrawGizmos()
        {
            var position = transform.position;
            
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(position, distanceToChase);
            
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(position, distanceToAttack);

            if (!player) return;
            var vectorToPlayer = player.position - position;
            
            Gizmos.color = Color.green;
            Gizmos.DrawRay(position, vectorToPlayer);

            var destination = vectorToPlayer.normalized * (vectorToPlayer.magnitude - 1);
            
            Gizmos.color = Color.red;
            Gizmos.DrawLine(position, destination + position);
        }
    }
}