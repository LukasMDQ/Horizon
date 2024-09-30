using UnityEngine;
using UnityEngine.AI;
using System.Collections;
// ReSharper disable InconsistentNaming

public class EnemyAi2 : MonoBehaviour
{
    [SerializeField] private Transform[] _patrolPoints; // Puntos de patrullaje aleatorio.
    [SerializeField] private Transform _player; // Referencia al objeto Player.
    [SerializeField] private float detectionRange = 10f; // Rango de detección del jugador.
    [SerializeField] private float attackRange = 2f; // Rango de ataque cuerpo a cuerpo.
    [SerializeField] private float distAttackRange = 6f; // Rango de ataque a distancia.


    [SerializeField] private NavMeshAgent _agent;
    [SerializeField] private Animator _animator;
    private int _currentPatrolIndex;
    private bool _isChasing;
    private bool _isAttacking;
    [SerializeField]
    bool _enemyDist = false;

    //ATAQUE A DISTANCIA 
    [SerializeField] float _fireCooldown = default;  // Duración del cooldown en segundos
    private bool isCooldown = false;
    [SerializeField] GameObject _eBullet;
    [SerializeField] Transform _spawnPoint;
    //ANIMACIONES
    private static readonly int Walk = Animator.StringToHash("walk");
    private static readonly int Run = Animator.StringToHash("run");
    private static readonly int AttackM = Animator.StringToHash("attackM");
    private static readonly int AttackD = Animator.StringToHash("attackD");

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
            MeleAttack();
        }
        if (distanceToPlayer <= distAttackRange)
        {
            // Atacar si está en rango
            DistanceAttack();
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
        SetAnimationBooleans(true, false, false,false); // Activa la animación de caminar (walk).
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
            SetAnimationBooleans(false, true, false, false); // Activa la animación de correr (run).
        }
        
        // Rotar hacia el jugador
        var playerPosition = _player.position;
        
        Vector3 directionToPlayer = (playerPosition - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(directionToPlayer.x, 0, directionToPlayer.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);

        _agent.destination = playerPosition;
    }

    private void MeleAttack()
    {
        if (!_isAttacking && !_enemyDist)
        {
            _isAttacking = true;
            _isChasing = false;
            _agent.isStopped = true; // Detener al enemigo para atacar.
            SetAnimationBooleans(false, false, true, false); // Activa la animación de ataque (attackM).
        }
    }
    private void DistanceAttack()
    {
        if (!_isAttacking && _enemyDist)
        {
            _isAttacking = true;
            _isChasing = false;
            _agent.isStopped = true; // Detener al enemigo para atacar.
            StartCoroutine(AutoFire());           
            SetAnimationBooleans(false, false, false, true); // Activar la animación de ataque (attackD).
        }
    }
    IEnumerator AutoFire()
    {
        while (true)
        {            
            Instantiate(_eBullet, _spawnPoint.position, _spawnPoint.rotation);

            // Espera el tiempo antes de instanciar otro objeto
            yield return new WaitForSeconds(_fireCooldown);
        }
    }

    // Configura los parámetros booleanos del Animator
    private void SetAnimationBooleans(bool walk, bool run, bool attackM, bool attackD)
    {
        _animator.SetBool(Walk, walk);
        _animator.SetBool(Run, run);
        _animator.SetBool(AttackM, attackM);
        _animator.SetBool(AttackD, attackD);
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