using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet_IA2 : MonoBehaviour
{
    [SerializeField] float _speed = default; 

    private Transform player;
    void Start()
    {        
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }
    
    void Update()
    {
        if (player != null)
        {
            // Calcula  distancia entre el objeto y el "Jugador"
            float distance = Vector3.Distance(transform.position, player.position);
            // Calcula la dirección hacia el "Player"
            Vector3 direction = (player.position - transform.position).normalized;

            // Mueve el objeto hacia el "Jugador"
            transform.position += direction * _speed * Time.deltaTime;          
            
        }       

    }
    private void OnTriggerEnter(Collider other)//DAÑO AL JUGADOR
    {
        if (other.CompareTag("Player")) destruct();
    }
    void destruct()
    {
        //Instantiate(explosion, transform.position, transform.rotation);
        Destroy(gameObject);
    }
}
