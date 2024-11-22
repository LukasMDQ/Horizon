using System.Collections;
using UnityEngine;
//TP2- Hernandez Lucas
public class TrapCover : MonoBehaviour
{
    private bool onCollision = false;
    private float collisionTime = 0f;
    [SerializeField]
    float timeToDestroy; 
    
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            onCollision = true;
        }
    }

    
    private void OnCollisionStay(Collision collision)
    {
        if (onCollision)
        {
            collisionTime += Time.deltaTime;

            if (collisionTime >= timeToDestroy)
            {
                Destroy(gameObject);
            }
        }
    }
   
    private void OnCollisionExit(Collision collision)//cuando termina la colisión
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            onCollision = false;
            collisionTime = 0f; // Reiniciar el contador si el player deja de colisionar
        }
    }
}
