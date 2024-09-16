using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

public class Carbine : MonoBehaviour
{
    public Transform spawnPoint;
    public GameObject bullet;
    [SerializeField] GameObject _FlashEffect, _instancePoint;
    public Stats stats;
    void Start()
    {
        
        //_stats = GetComponent<Stats>();
    }
    void Update()
    {
        Carabina();
    }
    private void flash()
    {
        Instantiate(_FlashEffect, _instancePoint.transform.position, Quaternion.identity);
    }
    void Carabina()
    {
       
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("ewfew");
            if (stats.bulletCount > 0)
            {
                flash();

                Instantiate(bullet, spawnPoint.position, spawnPoint.rotation);

                stats.bulletCount -= 1;

            }
            else
            {
                Debug.Log("SIN MUNICION");
            }
        }
    }
   
}
