using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public abstract class Entity : MonoBehaviour
{
    public float curHp, maxHp;
    public GameObject _destroyEffect, _drops;
    public AudioSource _spawnSound;
    public AudioClip[] _sounds;

    private void Start()
    {
        if (!_spawnSound) _spawnSound = gameObject.GetComponent<AudioSource>();

        curHp = maxHp;

        MyStart();
    }

    /// <summary>
    /// Use this instead of Start() to not overrite it
    /// </summary>
    protected abstract void MyStart();

    public virtual void TakeDamage(float damage)
    {
        curHp -= damage;      
        if (curHp <= 0)
        {
            Death();
        }
        else
        {
            if (_sounds.Length > 0) _spawnSound.PlayOneShot(_sounds[0]); // 0 = dmgSound
        }        
    }

    public virtual void Heal(int healPower)//CURAR
    {
        curHp += healPower;
        
        if (curHp > maxHp)
            curHp = maxHp;

        Debug.Log("Curado");
    }

    public virtual void Death()
    {
        if (_sounds.Length > 0) _spawnSound.PlayOneShot(_sounds[1]); // 1 = deathSound
        if(_destroyEffect != null)
        {
            RandomDrop();
            var myTransform = transform;
            Instantiate(_destroyEffect, myTransform.position, myTransform.rotation);
        }
        Destroy(gameObject);
    }

    private void RandomDrop()
    {
        int rdn = Random.Range(0, 100);
        
        if (rdn <= 50)
        {
            var myTransform = transform;
            Instantiate(_drops, myTransform.position, myTransform.rotation);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("WeaponPlayer"))
        {
            GameObject player = GameObject.FindWithTag("Player");
            Debug.Log(player);
            if (player != null && player.TryGetComponent(out Stats playerStats))
            {
                TakeDamage(playerStats.damage);
            }
        }
    }
}