using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Lidia Paiva
public class SpearTrap : MonoBehaviour
{
    [SerializeField] private Transform[] _spearsSpawner;
    [SerializeField] private GameObject spearPrefab;
    [SerializeField] private float spearCadence;


    public bool isActiveTrap = false;
    private Coroutine activeTrapCoroutine;

    private void Update()
    {
        if (isActiveTrap && activeTrapCoroutine == null)
        {
            activeTrapCoroutine = StartCoroutine(ActivateTrapContinuously());
        }

        if (!isActiveTrap && activeTrapCoroutine != null)
        {
            StopCoroutine(activeTrapCoroutine);
            activeTrapCoroutine = null; 
        }
    }

    private IEnumerator ActivateTrapContinuously()
    {
        while (isActiveTrap)
        {
            for (int i = 0; i < _spearsSpawner.Length; i++)
            {
               
                Instantiate(spearPrefab, _spearsSpawner[i].position, _spearsSpawner[i].rotation);
                
                yield return new WaitForSeconds(spearCadence);
            }
        }
    }

}
