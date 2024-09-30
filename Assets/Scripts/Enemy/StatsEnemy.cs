using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ReSharper disable once CheckNamespace
public class StatsEnemy : Stats
{   
    // ReSharper disable once InconsistentNaming
    [SerializeField] private GameObject _deathEffect;

    private void Start()
    {
        curHp = maxHp;        
    }

    private void Update()
    {
        Death();
    }

    private void Death()
    {
        if(curHp <=0)
        {
            var myTransform = transform;
            Instantiate(_deathEffect, myTransform.position, myTransform.rotation);
            Destroy(gameObject);
        }
    }
}