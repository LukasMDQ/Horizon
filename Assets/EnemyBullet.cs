using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    [SerializeField] private float _speed = default;
    public int damage = default;
    [SerializeField] float _lifeTime = default;
    [SerializeField] private GameObject _inpact;
    [SerializeField] bool _iA = false;
    private Transform _player;
    private void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player").transform; // TODO refactor this
        Destroy(gameObject, _lifeTime);
    }
    void Update()
    {
        if (_iA)
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
        else transform.Translate(Vector3.forward * _speed * Time.deltaTime);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Stats stats) && other.CompareTag("Player"))//Interactura con el enemigo y se destruye.
        {
            stats.TakeDamage(damage);
            DestructionBullet();
        }
        if (other.CompareTag("Static"))
        {
            DestructionBullet();// de destruye al impactar con el tag static (entorno).
        }
    }

    void DestructionBullet()
    {
        //Instantiate(_inpact, transform.position, transform.rotation);
        Destroy(gameObject);
    }

}
