using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(Animator))]
public abstract class SkeletonEnemy : Entity
{
    [SerializeField] private Transform[] _patrolPoints;
    
    [SerializeField] protected float detectionRange = 10f; // Rango de detección del jugador.
    [SerializeField] protected float attackRange = 2; // Rango de ataque

    private Transform _player; // Referencia al objeto Player.

    protected NavMeshAgent _agent;
    private   Animator     _animator;

    private   int  _currentPatrolIndex;
    protected bool _isChasing;
    protected bool _isAttacking;

    private static readonly int Walk    = Animator.StringToHash("walk");
    private static readonly int Run     = Animator.StringToHash("run");
    private static readonly int AttackM = Animator.StringToHash("attackM");
    private static readonly int AttackD = Animator.StringToHash("attackD");
    private static readonly int idleP = Animator.StringToHash("idle");

    /// <summary>
    /// Don't forget to define attackRange
    /// </summary>
    protected override void MyStart()
    {
        _player   = Movement3D.playerTransform;
        _agent    = GetComponent<NavMeshAgent>();
        _animator = GetComponent<Animator>();

        GotoNextPatrolPoint();
    }

    private void Update()
    {
        float distanceToPlayer = Vector3.Distance(_player.position, transform.position);

        if (distanceToPlayer <= attackRange)
        {
            Attack();
        }
        else if (distanceToPlayer <= detectionRange)
        {
            ChasePlayer();
        }
        else
        {
            Patrol();
        }
    }

    private void GotoNextPatrolPoint()
    {
        if (_patrolPoints.Length == 0) return;

        if(_agent != null)
        {
            _agent.destination = _patrolPoints[_currentPatrolIndex].position;
            _currentPatrolIndex = (_currentPatrolIndex + 1) % _patrolPoints.Length;
            SetAnimationBooleans(true, false, false, false, false); // Activa la animación de caminar (walk).
        }
       
    }

    private void Patrol()
    {
        if (_isChasing || _isAttacking)
        {
            _isChasing = false;
            _isAttacking = false;
            _agent.speed = 3.5f;
            _agent.isStopped = false;
        }

        if (_agent != null && !_agent.pathPending && _agent.remainingDistance < 0.5f)
        {
            GotoNextPatrolPoint();
        }
    }

    private void ChasePlayer()
    {
        if (!_isChasing)
        {
            _isChasing = true;
            _isAttacking = false;
            _agent.isStopped = false;
            _agent.speed = 6f;
            SetAnimationBooleans(false, true, false, false, false); // Activa la animación de correr (run).
        }

        // Rotar hacia el jugador
        var playerPosition = _player.position;

        Vector3 directionToPlayer = (playerPosition - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(directionToPlayer.x, 0, directionToPlayer.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);

        _agent.destination = playerPosition;
    }

    protected abstract void Attack();

    // Configura los parámetros booleanos del Animator
    protected void SetAnimationBooleans(bool walk, bool run, bool attackM, bool attackD, bool idle) // Can we change the booleans to triggers?
    {
        _animator.SetBool(Walk, walk);
        _animator.SetBool(Run, run);
        _animator.SetBool(AttackM, attackM);
        _animator.SetBool(AttackD, attackD);
        _animator.SetBool(idleP, idle);
    }

    public override void Death()
    {
        base.Death();
        if (curHp <= 0)
        {
            var myTransform = transform;
            Instantiate(_destroyEffect, myTransform.position, myTransform.rotation);
            Destroy(gameObject);
        }
    }

    private void OnDrawGizmosSelected()
    {
        // Dibuja los rangos de detección y ataque para depuración.
        var myPosition = transform.position;
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(myPosition, detectionRange);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(myPosition, attackRange);
    }
}