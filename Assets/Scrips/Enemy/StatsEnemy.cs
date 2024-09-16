using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatsEnemy : Stats
{   
    [SerializeField] private GameObject _deathEffect;
    void Start()
    {
        curHp = maxHp;        
    }
   
    void Update()
    {
        death();
    }
    void death ()
    {
        if(curHp <=0)
        {
            Instantiate(_deathEffect, transform.position, transform.rotation);
            Destroy(gameObject);
        }
    }
}
