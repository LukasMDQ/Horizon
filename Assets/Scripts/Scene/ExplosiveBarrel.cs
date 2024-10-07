using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosiveBarrel : Entity
{
    [SerializeField]
    GameObject _explodeEffect;

    [SerializeField]
    private float explosionRange;

    void Drop()
    {
        Instantiate(_drops, transform.position, transform.rotation);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, explosionRange);
    }

    void Explode()
    {
        Instantiate(_explodeEffect, transform.position, transform.rotation);

        Collider[] objectsToExplode = Physics.OverlapSphere(transform.position, explosionRange); 

        foreach (Collider objectToExplode in objectsToExplode)
        {
            Entity entity = objectToExplode.GetComponent<Entity>();
            if (entity != null && entity != this)
            {
                objectToExplode.GetComponent<Entity>().Death();
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
        Explode();
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

    protected override void MyStart()
    {
        
    }
}
