using UnityEngine;
using UnityEngine.AI;

public class SkeletonIA : MonoBehaviour
{
    public NavMeshAgent agent;            // Agente de navegación
    public Transform player;              // Referencia al jugador
    public LayerMask whatIsPlayer;        // Capa que contiene al jugador
    public GameObject projectile;         // Proyectil a instanciar en el ataque

    public float patrolRadius = 10f;      // Radio de patrulla aleatoria
    public float sightRange = 20f;        // Rango de visión
    public float attackRange = 5f;        // Rango de ataque
    public float timeBetweenAttacks = 2f; // Tiempo entre ataques
    public float idleTime = 3f;           // Tiempo de espera en estado idle

    private bool walkPointSet;            // Si ya tiene un punto de patrulla
    private Vector3 walkPoint;            // Punto al que patrullar
    private Animator anim;                // Referencia al Animator
    private bool alreadyAttacked;         // Control de ataque
    private float idleTimer;              // Temporizador para la animación idle
    private bool isIdle;                  // Controla si está en estado idle

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        idleTimer = idleTime;
        isIdle = false;
    }

    void Update()
    {
        // Verificar si el jugador está en rango de visión o ataque
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
            anim.SetBool("walk", false);         // Detener animación de caminar
            anim.SetBool("idle", true);             // Iniciar animación idle

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
                anim.SetBool("walk", true); // Activar animación de caminar
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

    // Perseguir al jugador cuando está dentro del rango de visión
    private void ChasePlayer()
    {
        agent.SetDestination(player.position);
        anim.SetBool("walk", false);  // Desactivar animación de caminar
        anim.SetBool("idle", false);     // Desactivar animación idle
        anim.SetBool("run", true);   // Activar animación de correr
    }

    // Atacar al jugador cuando está dentro del rango de ataque
    private void AttackPlayer()
    {
        // Detener al enemigo para atacar
        agent.SetDestination(transform.position);
        anim.SetBool("run", false);  // Desactivar animación de correr
        anim.SetBool("attackD", true); // Activar animación de ataque

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
        // Visualizar los rangos de visión y ataque en la escena
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, sightRange);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}
