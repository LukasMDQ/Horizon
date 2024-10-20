using UnityEngine;
using UnityEngine.AI;

public class SkeletonIA : MonoBehaviour
{
    public NavMeshAgent agent;            // Agente de navegaci�n
    public Transform player;              // Referencia al jugador
    public LayerMask whatIsPlayer;        // Capa que contiene al jugador
    public GameObject projectile;         // Proyectil a instanciar en el ataque

    public float patrolRadius = 10f;      // Radio de patrulla aleatoria
    public float sightRange = 20f;        // Rango de visi�n
    public float attackRange = 5f;        // Rango de ataque
    public float timeBetweenAttacks = 2f; // Tiempo entre ataques
    public float idleTime = 3f;           // Tiempo de espera en estado idle

    private bool walkPointSet;            // Si ya tiene un punto de patrulla
    private Vector3 walkPoint;            // Punto al que patrullar
    private Animator anim;                // Referencia al Animator
    private bool alreadyAttacked;         // Control de ataque
    private float idleTimer;              // Temporizador para la animaci�n idle
    private bool isIdle;                  // Controla si est� en estado idle

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        idleTimer = idleTime;
        isIdle = false;
    }

    void Update()
    {
        // Verificar si el jugador est� en rango de visi�n o ataque
        bool playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
        bool playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);

        if (!playerInSightRange && !playerInAttackRange) Patroling();
        //if (playerInSightRange && !playerInAttackRange) ChasePlayer();
        //if (playerInAttackRange) AttackPlayer();
    }

    // Patrullando aleatoriamente por el mapa
    private void Patroling()
    {
        if (isIdle)
        {
            agent.SetDestination(transform.position); // Detener agente
            anim.SetBool("walk", false);         // Detener animaci�n de caminar
            anim.SetBool("idle", true);             // Iniciar animaci�n idle

            // Temporizador para volver a patrullar
            idleTimer -= Time.deltaTime;
            if (idleTimer <= 0f)
            {
                isIdle = false;
                idleTimer = idleTime;
            }
        }
        else
        {
            if (!walkPointSet) SearchWalkPoint();

            if (walkPointSet)
            {
                agent.SetDestination(walkPoint);
                anim.SetBool("idle", false);
                anim.SetBool("walk", true); // Activar animaci�n de caminar
            }

            Vector3 distanceToWalkPoint = transform.position - walkPoint;

            // Si llega al punto de patrulla, detenerse
            if (distanceToWalkPoint.magnitude < 1f)
            {
                walkPointSet = false;
                isIdle = true;  // Cambiar a estado idle al llegar al punto
            }
        }
    }

    // Buscar un punto aleatorio dentro del radio de patrulla
    private void SearchWalkPoint()
    {
        float randomZ = Random.Range(-patrolRadius, patrolRadius);
        float randomX = Random.Range(-patrolRadius, patrolRadius);

        walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

        if (NavMesh.SamplePosition(walkPoint, out NavMeshHit hit, patrolRadius, 1))
        {
            walkPoint = hit.position;
            walkPointSet = true;
        }
    }

    // Perseguir al jugador cuando est� dentro del rango de visi�n
    private void ChasePlayer()
    {
        agent.SetDestination(player.position);
        anim.SetBool("walk", false);  // Desactivar animaci�n de caminar
        anim.SetBool("idle", false);     // Desactivar animaci�n idle
        anim.SetBool("run", true);   // Activar animaci�n de correr
    }

    // Atacar al jugador cuando est� dentro del rango de ataque
    private void AttackPlayer()
    {
        // Detener al enemigo para atacar
        agent.SetDestination(transform.position);
        anim.SetBool("run", false);  // Desactivar animaci�n de correr
        anim.SetBool("attackD", true); // Activar animaci�n de ataque

        if (!alreadyAttacked)
        {
            // Instanciar el proyectil
            Instantiate(projectile, transform.position + transform.forward, Quaternion.identity);
            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), timeBetweenAttacks);
        }
    }

    // Resetear el estado de ataque
    private void ResetAttack()
    {
        alreadyAttacked = false;
    }

    private void OnDrawGizmosSelected()
    {
        // Visualizar los rangos de visi�n y ataque en la escena
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, sightRange);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}
