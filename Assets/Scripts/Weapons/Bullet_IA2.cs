using UnityEngine;

public class Bullet_IA2 : MonoBehaviour
{
    [SerializeField] private float _speed;

    private Transform _player;

    private void Start()
    {        
        _player = GameObject.FindGameObjectWithTag("Player").transform; // TODO refactor this
    }

    private void Update()
    {
        if (_player != null)
        {
            // Calcula  distancia entre el objeto y el "Jugador"
            float distance = Vector3.Distance(transform.position, _player.position);
            // Calcula la dirección hacia el "Player"
            Vector3 direction = (_player.position - transform.position).normalized;

            // Mueve el objeto hacia el "Jugador"
            transform.position += direction * (_speed * Time.deltaTime);          
            
        }
    }
    
    private void OnTriggerEnter(Collider other)//DAÑO AL JUGADOR
    {
        if (other.CompareTag("Player")) Destruct();
    }

    private void Destruct()
    {
        //Instantiate(explosion, transform.position, transform.rotation);
        Destroy(gameObject);
    }
}