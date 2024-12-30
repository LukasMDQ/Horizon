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
        [HideInInspector] public BossPatrolling patrollingState;
        [HideInInspector] public BossPursuing pursuingState;
        [HideInInspector] public BossAttacking attackingState;
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

        public Transform player;

        [Tooltip("Layers I can see")]
        public LayerMask layerMask;

        public Animator animator;
        private static readonly int Patrolling = Animator.StringToHash("Patrolling");
        private static readonly int Pursuing   = Animator.StringToHash("Pursuing");
        private static readonly int Attacking  = Animator.StringToHash("Attacking");
        private static readonly int AttackRng  = Animator.StringToHash("AttackRNG");
        private static readonly int GotHit     = Animator.StringToHash("GotHit");
        private static readonly int Dead       = Animator.StringToHash("Dead");

        private void Awake()
        {
            var myTransform = transform;
            patrollingState = new BossPatrolling(this, myTransform, player, agent, speed, distanceToChangeWaypoint, distanceToChase, waypoints, layerMask);
            pursuingState = new BossPursuing(this, myTransform, player, agent, speed, speedMultiplier, distanceToAttack, layerMask);
            attackingState = new BossAttacking(this, myTransform, player, distanceToAttack);
            deathState = new Death(this, agent);
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
                    animator.SetInteger(AttackRng, Random.Range(0, 3));
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

        public bool IsDead()
        {
            return animator.GetBool(Dead);
        }
        
        // ReSharper disable once UnusedMember.Local
        private void ResetAttack() // This should be called from an animation event
        {
            attackingState.ResetAttack();
        }

        public void ReactAttack()
        {
            if (CurrentState != attackingState && CurrentState != deathState)
            {
                ChangeState(pursuingState);
            }
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