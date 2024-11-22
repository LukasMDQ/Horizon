using System.Collections;
using UnityEngine;
//TP2- Hernandez Lucas
public class Instakill:MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        var instaKill = other.GetComponent<IinstaKill>();
        {
            if (instaKill != null)
            {
                instaKill.Death();
            }
        }
    }
}

