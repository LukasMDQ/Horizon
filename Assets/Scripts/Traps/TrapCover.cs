using System.Collections;
using UnityEngine;

public class TrapCover : MonoBehaviour
{
    private bool onCollision = false;
    private float collisionTime = 0f;
    [SerializeField]
     float timeToDestroy = 3f; // Tiempo para destruir el objeto
    
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            onCollision = true;
        }
    }

    
    private void OnCollisionStay(Collision collision)// mientars este en colision
    {
        if (onCollision)
        {
            collisionTime += Time.deltaTime;

            if (collisionTime >= timeToDestroy)
            {
                Destroy(gameObject); // Destruir el objeto después de 3 segundos 
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
