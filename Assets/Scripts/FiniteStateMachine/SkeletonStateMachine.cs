using System;
using FiniteStateMachine.States;
using UnityEngine;
using UnityEngine.AI;

namespace FiniteStateMachine
{
	[RequireComponent(typeof(NavMeshAgent))]
	public class SkeletonStateMachine : StateMachine
	{
		[HideInInspector] public SkeletonPatrolling patrollingState;
		[HideInInspector] public SkeletonPursuing pursuingState;
		[HideInInspector] public SkeletonAttacking attackingState;
		[HideInInspector] public Death deathState;

		public Transform[] waypoints;
		public NavMeshAgent agent;

		public float speed;
		[Tooltip("Tolerance to change waypoint")] [Range(0f, 1.5f)]
		public float distanceToChangeWaypoint;
		public float distanceToChase;
		public float distanceToAttack;

		public Transform player;
    
		[Tooltip("Layers I can see")]
		public LayerMask layerMask;

		public Animator animator;
		private static readonly int Walk    = Animator.StringToHash("walk");
		private static readonly int Run     = Animator.StringToHash("run");
		private static readonly int AttackM = Animator.StringToHash("attackM");
		private static readonly int AttackD = Animator.StringToHash("attackD");
		private static readonly int Hit     = Animator.StringToHash("hit");
		private static readonly int DeadM   = Animator.StringToHash("deadM");

		protected SkeletonAttackType skeletonAttackType;

		protected virtual void Awake()
		{
			var myTransform = transform;
			patrollingState = new SkeletonPatrolling(this, myTransform, player, agent, speed, distanceToChangeWaypoint, distanceToChase, waypoints, layerMask, skeletonAttackType);
			pursuingState = new SkeletonPursuing(this, myTransform, player, agent, speed, distanceToAttack, layerMask, skeletonAttackType);
			attackingState = new SkeletonAttacking(this, myTransform, player, distanceToAttack, skeletonAttackType);
			deathState = new Death(this, agent);
		}

		protected override BaseState GetInitialState()
		{
			return patrollingState;
		}
		
		public void SetSkeletonAnimations(SkeletonAnimationsType animationsType)
		{
			switch (animationsType)
			{
				case SkeletonAnimationsType.Idle:
					animator.SetBool(Walk, false);
					animator.SetBool(Run, false);
					animator.SetBool(AttackM, false);
					animator.SetBool(AttackD, false);
					animator.SetBool(Hit, false);
					break;
				case SkeletonAnimationsType.Patrolling:
					animator.SetBool(Walk, true);
					animator.SetBool(Run, false);
					animator.SetBool(AttackM, false);
					animator.SetBool(AttackD, false);
					animator.SetBool(Hit, false);
					break;
				case SkeletonAnimationsType.Pursuing:
					animator.SetBool(Walk, false);
					animator.SetBool(Run, true);
					animator.SetBool(AttackM, false);
					animator.SetBool(AttackD, false);
					animator.SetBool(Hit, false);
					break;
				case SkeletonAnimationsType.AttackM:
					animator.SetBool(Walk, false);
					animator.SetBool(Run, false);
					animator.SetBool(AttackM, true);
					animator.SetBool(AttackD, false);
					animator.SetBool(Hit, false);
					break;
				case SkeletonAnimationsType.AttackD:
					animator.SetBool(Walk, false);
					animator.SetBool(Run, false);
					animator.SetBool(AttackM, false);
					animator.SetBool(AttackD, true);
					animator.SetBool(Hit, false);
					break;
				case SkeletonAnimationsType.GetHit:
					animator.SetBool(Walk, false);
					animator.SetBool(Run, false);
					animator.SetBool(AttackM, false);
					animator.SetBool(AttackD, false);
					animator.SetBool(Hit, true);
					break;
				case SkeletonAnimationsType.Death:
					animator.SetBool(Walk, false);
					animator.SetBool(Run, false);
					animator.SetBool(AttackM, false);
					animator.SetBool(AttackD, false);
					animator.SetBool(Hit, false);
					animator.SetBool(DeadM, true);
					break;
				default:
					throw new ArgumentOutOfRangeException(nameof(animationsType), animationsType, null);
			}
		}
		
		public enum SkeletonAnimationsType
		{
			Idle,
			Patrolling,
			Pursuing,
			AttackM,
			AttackD,
			GetHit,
			Death
		}

		public enum SkeletonAttackType
		{
			Melee,
			Distance
		}
		
		public bool IsDead()
		{
			return animator.GetBool(DeadM);
		}
		
		private void OnDrawGizmos()
		{
			var position = transform.position;
            
			Gizmos.color = Color.green;
			Gizmos.DrawWireSphere(position, distanceToChase);
            
			Gizmos.color = Color.red;
			Gizmos.DrawWireSphere(position, distanceToAttack);
		}
	}
}