using System.Collections;
using UnityEngine;

public class Instakill : MonoBehaviour
{
    [SerializeField] int _damage = default;
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Stats stats) && other.CompareTag("Player"))
        {
            stats.TakeDamage(_damage);
        }
    }
}

