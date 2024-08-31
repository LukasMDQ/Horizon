using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivarArma : MonoBehaviour
{
    public PickUp pickUp;
    public int numeroArma;
    void Start()
    {
        pickUp = GameObject.FindGameObjectWithTag("Player").GetComponent<PickUp>();
    }

   
    void Update()
    {
        
    }
    private void OnCollisionStay(Collision other)
    {
        if (other.gameObject.name == "Player") 
        {
            pickUp.ActivarArma(numeroArma);
            Destroy(gameObject);
        }
    }
}
