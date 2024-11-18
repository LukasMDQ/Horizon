using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public abstract class Drop : MonoBehaviour
{
    public AudioClip collisionSound;
    public virtual void ApplyDropEffect(Collider other) {  }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Trigger", other);
        ApplyDropEffect(other);

        Destruction();
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