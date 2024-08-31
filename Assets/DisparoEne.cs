using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisparoEne : MonoBehaviour
{
    public GameObject eRocket;
    public GameObject eBullet;
    [SerializeField] private float _fireRate = 2;
    [SerializeField] float _timer = 0;
    [SerializeField] private bool _rocket = false;
    
    //public Stats stats;    

    private void Update()
    {
        shoot();
    }
    public void shoot()
    {
        if (_timer <= _fireRate)//timer
        {
            _timer += Time.deltaTime;
        }
        else
        {
            _timer = 0;
            if (!_rocket)//si es falso instancia un bullet
            {
                Instantiate(eBullet, transform.position, Quaternion.identity);
            }
            else//si es verdadero instanciara un rocket
            {
                Instantiate(eRocket, transform.position, Quaternion.identity);
            }
        }
    }
}
