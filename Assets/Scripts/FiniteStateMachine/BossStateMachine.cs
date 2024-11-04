using System;
using FiniteStateMachine.States;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

namespace FiniteStateMachine
{
    [RequireComponent(typeof(NavMeshAgent))]
    public class BossStateMachine : StateMachine
    {
        [HideInInspector] public Patrolling patrollingState;
        [HideInInspector] public Pursuing pursuingState;
        [HideInInspector] public Attacking attackingState;
        [HideInInspector] public Death deathState;

        public Transform[] waypoints;
        public NavMeshAgent agent;

        public float speed;
        [Tooltip("The speed multiplier when chasing")] [Range(1, 2)]
        public float speedMultiplier = 1.5f;
        [Tooltip("Tolerance to change waypoint")] [Range(0f, 1.5f)]
        public float distanceToChangeWaypoint;
        public float distanceToChase;
        public float distanceToAttack;
        public Collider attackArea;

        public Transform player;

        [Tooltip("Layers I can see")]
        public LayerMask layerMask;
        
        // This could work, but I have no idea
        /*[Tooltip("Time needed for the attack to occur")]
        [Range(0f, 10f)]
        public float timeToAttack;*/

        public Animator animator;
        private static readonly int Patrolling = Animator.StringToHash("Patrolling");
        private static readonly int Pursuing = Animator.StringToHash("Pursuing");
        private static readonly int Attacking = Animator.StringToHash("Attacking");
        private static readonly int GotHit = Animator.StringToHash("GotHit");
        private static readonly int Dead = Animator.StringToHash("Dead");

        private void Awake()
        {
            var myTransform = transform;
            patrollingState = new Patrolling(this, myTransform, player, agent, speed, distanceToChangeWaypoint, distanceToChase, waypoints, layerMask);
            pursuingState = new Pursuing(this, myTransform, player, agent, speed, speedMultiplier, distanceToAttack, distanceToChase, layerMask);
            attackingState = new Attacking(this, myTransform, player, distanceToAttack, attackArea);
            deathState = new Death(this);
        }

        protected override BaseState GetInitialState()
        {
            return patrollingState;
        }

        public void SetBossAnimations(BossAnimationsType animationsType)
        {
            switch (animationsType)
            {
                case BossAnimationsType.Idle:
                    animator.SetBool(Patrolling, false);
                    animator.SetBool(Pursuing, false);
                    break;
                case BossAnimationsType.Patrolling:
                    animator.SetBool(Patrolling, true);
                    animator.SetBool(Pursuing, false);
                    break;
                case BossAnimationsType.Pursuing:
                    animator.SetBool(Pursuing, true);
                    break;
                case BossAnimationsType.Attack:
                    animator.SetTrigger(Attacking);
                    animator.SetBool(Patrolling, false);
                    animator.SetBool(Pursuing, false);
                    break;
                case BossAnimationsType.GetHit:
                    animator.SetTrigger(GotHit);
                    break;
                case BossAnimationsType.Death:
                    animator.SetBool(Patrolling, false);
                    animator.SetBool(Pursuing, false);
                    animator.SetBool(Dead, true);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(animationsType), animationsType, null);
            }
        }

        public enum BossAnimationsType
        {
            Idle,
            Patrolling,
            Pursuing,
            Attack,
            GetHit,
            Death
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