using UnityEngine;
using System.Collections;
//TP2- Hernandez Lucas

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
        Stats stats = other.GetComponent<Stats>();
        
        if (stats != null)
        {
           stats.TakeDamage (dps * Time.deltaTime);
        }
    }
}
