using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//TP2- Hernandez Lucas

public abstract class Drop : MonoBehaviour
{
    public AudioClip collisionSound;
    public virtual void ApplyDropEffect(Collider other) {
        Destruction();
    }

    private void OnTriggerEnter(Collider other)
    {
        ApplyDropEffect(other);
    }

    private void Destruction() //instancia efecto y destruye el prefab
    {
        if (collisionSound != null)
        {
            AudioSource.PlayClipAtPoint(collisionSound, transform.position);// Instancia un sonido y reproduce el clip
        }        
        Destroy(gameObject);
    }
}