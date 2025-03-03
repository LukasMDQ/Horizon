using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using static UnityEditor.Experimental.GraphView.GraphView;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float _speed = default;
    public int damage;
    public int buffBullet = 2; 
    [SerializeField] float _lifeTime = default;      
    [SerializeField] private GameObject _inpact;
    public static bool IsBuffed = false;


    private void Start()
    {
        Destroy(gameObject, _lifeTime);
    }
    void Update()
    {
         transform.Translate(Vector3.forward * _speed * Time.deltaTime);       
    }
    private void OnTriggerEnter(Collider other)
    {        
        if (other.TryGetComponent(out Entity entity) && !other.CompareTag("Player"))
        {
            BuffBulletDamage();
            Debug.Log($"BulletDamage {damage} and is buffed {IsBuffed}");
            entity.TakeDamage(damage);
            DestructionBullet();
        }
        if (other.CompareTag("Static"))
        {            
            DestructionBullet();// Se destruye al impactar con el tag static (entorno).
        }
    }

    public void BuffBulletDamage()
    {
        if (IsBuffed)
        {
            damage *= buffBullet;
        } 
    }
      
    void DestructionBullet()
    {
        Instantiate(_inpact, transform.position, transform.rotation);
        Destroy(gameObject);
    }

}
