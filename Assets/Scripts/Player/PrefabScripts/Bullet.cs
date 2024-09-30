using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float _speed = default;
    public int damage = default;
    [SerializeField] float _lifeTime = default;      
    [SerializeField] private GameObject _inpact;
   
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
        if (other.TryGetComponent(out Stats stats) && other.CompareTag("Enemy"))//Interactura con el enemigo y se destruye.
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
        Instantiate(_inpact, transform.position, transform.rotation);
        Destroy(gameObject);
    }

}
