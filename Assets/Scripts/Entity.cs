using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Entity : MonoBehaviour
{
    public float curHp, maxHp = default;
    [SerializeField] GameObject _destroyEffect;
    private AudioSource _spawnSound = default;
    [SerializeField] private AudioClip[] _sounds = default;

    private void Start()
    {
        if (!_spawnSound) _spawnSound = gameObject.GetComponent<AudioSource>();
    }

    public virtual void TakeDamage(float damage)
    {
        Debug.Log("Entre");
        curHp -= damage;      
        if (curHp <= 0)
        {
            Death();
        } else
        {
            if (_sounds.Length > 0) _spawnSound.PlayOneShot(_sounds[0]); // 0 = dmgSound
        }
        
    }
    public virtual void Heal(int HealPower)//CURAR
    {
        curHp += HealPower;
        Debug.Log("Curado");
    }

    public virtual void Death()//MUERTE
    {
        if (_sounds.Length > 0) _spawnSound.PlayOneShot(_sounds[1]); // 1 = deathSound
        if(_destroyEffect != null)
        {
            Instantiate(_destroyEffect, transform.position, transform.rotation);
        }
        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Busca el GameObject con el tag "Player"
            GameObject player = GameObject.FindWithTag("Player");

            // Si se encuentra el GameObject "Player" y tiene un componente Stats
            if (player != null && player.TryGetComponent(out Stats playerStats))
            {
                // Aplica el daño usando la referencia de las estadísticas del jugador
                TakeDamage(playerStats.damage);
            }
        }
    }
}
