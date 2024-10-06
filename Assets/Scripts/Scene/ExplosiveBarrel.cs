using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosiveBarrel : Entity
{
    [SerializeField]
    GameObject _explodeEffect;

    [SerializeField]
    private float range;

    void Drop()
    {
        Instantiate(_drops, transform.position, transform.rotation);
    }

    void Explode()
    {
        Instantiate(_explodeEffect, transform.position, transform.rotation);

        Collider[] entities = Physics.OverlapSphere(transform.position, range); 

        foreach (Collider entity in entities)
        {
            if(entity.GetComponent<Entity>() != null)
            {
               entity.GetComponent<Entity>().Death();
            }
        }
    }
    void RandomEffectOnDestroy()
    {
        int randomChance = Random.Range(0, 101);

        if (randomChance <= 50) 
        {
            Explode(); 
        }
        else if (randomChance > 50 && randomChance <= 80) 
        {
            Drop(); 
        }
        else 
        {
            Instantiate(_destroyEffect, transform.position, transform.rotation);
        }
    }

    public override void Death()
    {
        if (_sounds.Length > 0) _spawnSound.PlayOneShot(_sounds[1]); // 1 = deathSound
        if (_destroyEffect != null)
        {
            RandomEffectOnDestroy();           
        }
        Destroy(gameObject);
    }

}
