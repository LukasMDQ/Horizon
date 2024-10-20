using UnityEngine;

public class LavaTrap : MonoBehaviour
{
    public float dps = default; // daño por segundo
    public float slow = default; // Cantidad de slow aplicado

    private void OnTriggerStay(Collider other)
    {
       
        Stats stats = other.GetComponent<Stats>();
        Movement3D movement3D = other.GetComponent<Movement3D>();


        if (stats != null)
        {
            stats.TakeDamage (dps * Time.deltaTime);
           
        }
        if (movement3D != null)
        {
            movement3D.Slow(slow);
        }
    }
}
