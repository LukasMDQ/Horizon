using UnityEngine;
using UnityEngine.AI;
// ReSharper disable InconsistentNaming

public class EnemyAi2 : MonoBehaviour
{
    [SerializeField] private Transform[] _patrolPoints; // Puntos de patrullaje aleatorio.
    [SerializeField] private Transform _player; // Referencia al objeto Player.
    [SerializeField] private float detectionRange = 10f; // Rango de detección del jugador.
    [SerializeField] private float attackRange = 2f; // Rango de ataque cuerpo a cuerpo.
    
    [SerializeField] private NavMeshAgent _agent;
    [SerializeField] private Animator _animator;
    private int _currentPatrolIndex;
    private bool _isChasing;
    private bool _isAttacking;
    
    private static readonly int Walk = Animator.StringToHash("walk");
    private static readonly int Run = Animator.StringToHash("run");
    private static readonly int AttackM = Animator.StringToHash("attackM");

    private void Start()
    {
        if (!_agent)
        {
            _agent = GetComponent<NavMeshAgent>();
        }

        if (!_animator)
        {
            _animator = GetComponent<Animator>();
        }
        
        GotoNextPatrolPoint(); // Iniciar patrullaje
    }

    private void Update()
    {
        float distanceToPlayer = Vector3.Distance(_player.position, transform.position);

        if (distanceToPlayer <= attackRange)
        {
            // Atacar si está en rango
            AttackPlayer();
        }
        else if (distanceToPlayer <= detectionRange)
        {
            // Perseguir al jugador si está en rango de detección
            ChasePlayer();
        }
        else
        {
            // Patrullar si el jugador no está en rango
            Patrol();
        }
    }

    private void GotoNextPatrolPoint()
    {
        if (_patrolPoints.Length == 0) return;

        _agent.destination = _patrolPoints[_currentPatrolIndex].position;
        _currentPatrolIndex = (_currentPatrolIndex + 1) % _patrolPoints.Length;
        SetAnimationBooleans(true, false, false); // Activa la animación de caminar (walk).
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

        // Si el agente llega al punto de patrullaje, cambia al siguiente
        if (!_agent.pathPending && _agent.remainingDistance < 0.5f)
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
            _agent.speed = 6f; // Velocidad de persecución.
            SetAnimationBooleans(false, true, false); // Activa la animación de correr (run).
        }
        
        // Rotar hacia el jugador
        var playerPosition = _player.position;
        
        Vector3 directionToPlayer = (playerPosition - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(directionToPlayer.x, 0, directionToPlayer.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);

        _agent.destination = playerPosition;
    }

    private void AttackPlayer()
    {
        if (!_isAttacking)
        {
            _isAttacking = true;
            _isChasing = false;
            _agent.isStopped = true; // Detener al enemigo para atacar.
            SetAnimationBooleans(false, false, true); // Activa la animación de ataque (attackM).
        }
    }

    // Configura los parámetros booleanos del Animator
    private void SetAnimationBooleans(bool walk, bool run, bool attackM)
    {
        _animator.SetBool(Walk, walk);
        _animator.SetBool(Run, run);
        _animator.SetBool(AttackM, attackM);
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