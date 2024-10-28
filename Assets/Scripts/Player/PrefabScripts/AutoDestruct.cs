using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoDestruct : MonoBehaviour
{
    [SerializeField] float _lifeTime = default;
   
    void Update()
    {
        Destroy(gameObject, _lifeTime);
    }    
}
