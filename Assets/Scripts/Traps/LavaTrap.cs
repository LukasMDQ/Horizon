using UnityEngine;
using System.Collections;
//TP FINAL- Hernandez Lucas

public class LavaTrap : MonoBehaviour
{
    public float dps = default; // daño por segundo
    public float duration = 1.5f;// duracion del objeto
    private void Start()
    {
        Destroy(gameObject, duration);
    }
    private void OnTriggerStay(Collider other)
    {
        //Stats stats = other.GetComponent<Stats>();
        var isDamageable = other.GetComponent<IDamageable>();
        {
            if (isDamageable != null)
            {
                isDamageable.TakeDamage(dps * Time.deltaTime);
            }
        }

        //if (stats != null)
        //{
        //   stats.TakeDamage (dps * Time.deltaTime);
        //}
    }
}
