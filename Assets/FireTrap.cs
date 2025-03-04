using UnityEngine;
using System.Collections;
using System.Net;

public class ToggleObjectCoroutine : MonoBehaviour
{
    public GameObject FireDamage;
    public float toggleTime = 2f; // Tiempo entre activaciones/desactivaciones

    void Start()
    {
        
    }
    private void OnTriggerStay(Collider other)
    {
        Stats stats = other.GetComponent<Stats>();

        if (stats != null)
        {
            StartCoroutine(ToggleLoop());
        }
        else FireDamage.SetActive(false);
    }

    IEnumerator ToggleLoop()
    {
        while (true)
        {
            yield return new WaitForSeconds(toggleTime);
            FireDamage.SetActive(!FireDamage.activeSelf);
        }
    }
   
}
