using UnityEngine;

[RequireComponent(typeof(AudioSource))]
// ReSharper disable once CheckNamespace

// Lidia Paiva
public abstract class Entity : Rewind
{
    public float curHp, maxHp;
    public GameObject _destroyEffect, _drops;
    public AudioSource _spawnSound;
    public AudioClip[] _sounds;
    public bool drop;

    protected virtual void Start()
    {
        if (!_spawnSound) _spawnSound = gameObject.GetComponent<AudioSource>();

        curHp = maxHp;
    }

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

    public virtual void Heal(int healPower)
    {
        curHp += healPower;
        
        if (curHp > maxHp)
            curHp = maxHp;

    }

    public virtual void Death()
    {
        if (_sounds.Length > 0) _spawnSound.PlayOneShot(_sounds[1]); // 1 = deathSound
        if (_drops != null)
        {
            RandomDrop();
        }
        if(_destroyEffect != null)
        {
            var myTransform = transform;
            Instantiate(_destroyEffect, myTransform.position, myTransform.rotation);
        }
        Destroy(gameObject);
    }

    private void RandomDrop()
    {
        if (!drop) return;
        
        var rdn = Random.Range(0, 100);

        if (rdn <= 50)
        {
            var myTransform = transform;
            Instantiate(_drops, myTransform.position, myTransform.rotation);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("WeaponPlayer")) return;
        
        var player = GameObject.FindWithTag("Player"); // TODO refactor later
        
        if (player != null && player != gameObject && player.TryGetComponent(out Stats playerStats))
        {
            TakeDamage(playerStats.damage);
        }
    }

    public override void Load()
    {
        throw new System.NotImplementedException();
    }

    public override void Save()
    {
        throw new System.NotImplementedException();
    }
}