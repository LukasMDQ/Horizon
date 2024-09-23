using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Entity : MonoBehaviour
{
    public float curHp, maxHp = default;
    [SerializeField] GameObject _destroyEffect, _drops;
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
            RandomDrop();
            Instantiate(_destroyEffect, transform.position, transform.rotation);
        }
        Destroy(gameObject);
    }
    void RandomDrop()
    {
        int rdn;
        rdn= Random.Range(0, 101);
        if(rdn<=50) Instantiate(_drops, transform.position, transform.rotation);
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("aja");
        if (other.CompareTag("WeaponPlayer"))
        {
            Debug.Log("Hola");
            // Busca el GameObject con el tag "Player"
            GameObject player = GameObject.FindWithTag("Player");
            Debug.Log(player);
            // Si se encuentra el GameObject "Player" y tiene un componente Stats
            if (player != null && player.TryGetComponent(out Stats playerStats))
            {
                Debug.Log("Holaaaaa");
                // Aplica el daño usando la referencia de las estadísticas del jugador
                TakeDamage(playerStats.damage);
            }
        }
    }
}
