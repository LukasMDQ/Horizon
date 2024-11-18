using UnityEngine;

/// <summary>
/// Class <c>ExplosiveBarrel</c> create the behaviour of explosive barrels, 
/// please don't put in scene an explosive asset next to other explosive asset for avoid excessive resource consumption.
/// </summary>
// ReSharper disable once CheckNamespace
public class ExplosiveBarrel : Entity
{
    [SerializeField] private GameObject _explodeEffect;

    [SerializeField]
    private float explosionRange;

    private void Drop()
    {
        var myTransform = transform;
        Instantiate(_drops, myTransform.position, myTransform.rotation);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, explosionRange);
    }

    private void Explode()
    {
        var myTransform = transform;
        Instantiate(_explodeEffect, myTransform.position, myTransform.rotation);

        // ReSharper disable once SuggestVarOrType_Elsewhere
        // ReSharper disable once Unity.PreferNonAllocApi
        Collider[] objectsToExplode = Physics.OverlapSphere(transform.position, explosionRange); 

        foreach (var objectToExplode in objectsToExplode)
        {
            var entity = objectToExplode.GetComponent<Entity>();
            if (entity != null && entity != this)
            {
                entity.Death();
                //objectToExplode.GetComponent<Entity>().Death();
            }

            var player = objectToExplode.GetComponent<Stats>();
            if (player != null)
            {
                player.TakeDamage(50);
                //objectToExplode.GetComponent<Stats>().TakeDamage(50);
            }
        }
    }

    private void RandomEffectOnDestroy()
    {
        var randomChance = Random.Range(0, 100);

        if (randomChance < 30)
        {
            Drop();
        }
        else
        {
            var myTransform = transform;
            Instantiate(_destroyEffect, myTransform.position, myTransform.rotation);
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
}