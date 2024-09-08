using UnityEngine;
using UnityEngine.AI;

public class EnemyAi2 : MonoBehaviour
{
    public Transform[] patrolPoints; // Puntos de patrullaje aleatorio.
    public Transform player; // Referencia al objeto Player.
    public float detectionRange = 10f; // Rango de detección del jugador.
    public float attackRange = 2f; // Rango de ataque cuerpo a cuerpo.

    private NavMeshAgent agent;
    private Animator animator;
    private int currentPatrolIndex;
    private bool isChasing = false;
    private bool isAttacking = false;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        GotoNextPatrolPoint(); // Iniciar patrullaje
    }

    void Update()
    {
        float distanceToPlayer = Vector3.Distance(player.position, transform.position);

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

    void GotoNextPatrolPoint()
    {
        if (patrolPoints.Length == 0) return;

        agent.destination = patrolPoints[currentPatrolIndex].position;
        currentPatrolIndex = (currentPatrolIndex + 1) % patrolPoints.Length;
        SetAnimationBooleans(true, false, false); // Activa la animación de caminar (walk).
    }

    void Patrol()
    {
        if (isChasing || isAttacking)
        {
            isChasing = false;
            isAttacking = false;
            agent.speed = 3.5f; 
            agent.isStopped = false;
        }

        // Si el agente llega al punto de patrullaje, cambia al siguiente
        if (!agent.pathPending && agent.remainingDistance < 0.5f)
        {
            GotoNextPatrolPoint();
        }
    }

    void ChasePlayer()
    {
        if (!isChasing)
        {
            isChasing = true;
            agent.speed = 6f; // Velocidad de persecución.
            SetAnimationBooleans(false, true, false); // Activa la animación de correr (run).
        }
        // Rotar hacia el jugador
        Vector3 directionToPlayer = (player.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(directionToPlayer.x, 0, directionToPlayer.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);

        
        agent.destination = player.position;
    }

    void AttackPlayer()
    {
        if (!isAttacking)
        {
            isAttacking = true;
            agent.isStopped = true; // Detener al enemigo para atacar.
            SetAnimationBooleans(false, false, true); // Activa la animación de ataque (attackM).
        }
    }

    // Configura los parámetros booleanos del Animator
    void SetAnimationBooleans(bool walk, bool run, bool attackM)
    {
        animator.SetBool("walk", walk);
        animator.SetBool("run", run);
        animator.SetBool("attackM", attackM);
    }

    void OnDrawGizmosSelected()
    {
        // Dibuja los rangos de detección y ataque para depuración.
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, detectionRange);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}
