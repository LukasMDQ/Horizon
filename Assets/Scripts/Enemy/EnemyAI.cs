using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    public Transform[] waypoints; // Puntos por los que el enemigo se moverá de forma aleatoria.
    public float detectionRange = 10f; // Distancia máxima a la que el enemigo puede detectar al jugador.
    public float attackRange = 2f; // Distancia mínima para iniciar el ataque.
    public float patrolWaitTime = 3f; // Tiempo que espera antes de cambiar de waypoint.
    public Transform player; // Referencia al jugador.

    private NavMeshAgent agent;
    private Animator animator;
    private int currentWaypointIndex = 0;
    private bool isChasing = false;
    private bool isAttacking = false;
    private float patrolTimer = 0f;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        GotoNextPoint();
    }

    void Update()
    {
        float distanceToPlayer = Vector3.Distance(player.position, transform.position);

        if (distanceToPlayer <= attackRange)
        {
            // Si está en rango de ataque.
            AttackPlayer();
        }
        else if (distanceToPlayer <= detectionRange)
        {
            // Si el jugador está dentro del rango de visión, lo persigue.
            ChasePlayer();
        }
        else
        {
            // Si no detecta al jugador, sigue patrullando.
            Patrol();
        }
    }

    void GotoNextPoint()
    {
        animator.SetBool("walk", true);
        if (waypoints.Length == 0)
            return;

        agent.destination = waypoints[currentWaypointIndex].position;
        currentWaypointIndex = (currentWaypointIndex + 1) % waypoints.Length;
        animator.Play("walk"); // Cambia a la animación de caminar.
    }    

    void Patrol()
    {
        if (isChasing)
        {
            //animator.SetBool("walk", true);
            isChasing = false;
            agent.speed = 3.5f; // Velocidad de patrulla.
            animator.Play("walk"); // Cambia a la animación de caminar.
        }

        // Si el enemigo ha llegado a su destino.
        if (!agent.pathPending && agent.remainingDistance < 0.5f)
        {
            patrolTimer += Time.deltaTime;
            if (patrolTimer >= patrolWaitTime)
            {
                patrolTimer = 0f;
                GotoNextPoint();
            }
        }
    }

    void ChasePlayer()
    {
        if (!isChasing)
        {
            animator.SetBool("run", true);
            isChasing = true;
            agent.speed = 6f; // Aumenta la velocidad al perseguir.
            animator.Play("run"); // Cambia a la animación de correr.
        }

        agent.destination = player.position;
    }

    void AttackPlayer()
    {
        if (!isAttacking)
        {   
            animator.SetBool("attackM", true);
            isAttacking = true;
            agent.isStopped = true; // Detiene al enemigo.
            animator.Play("attackM"); // Cambia a la animación de ataque.
            StartCoroutine(AttackCooldown());
        }
        else animator.SetBool("attackM", false);
    }

    IEnumerator AttackCooldown()
    {
        yield return new WaitForSeconds(1f); // Simula la duración del ataque.
        isAttacking = false;
        agent.isStopped = false; // Reactiva el movimiento del enemigo después del ataque.
    }

    void OnDrawGizmosSelected()
    {
        // Dibuja el rango de detección y el rango de ataque para depurar.
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, detectionRange);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}
