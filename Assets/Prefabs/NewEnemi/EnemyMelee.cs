using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
// ReSharper disable InconsistentNaming

// ReSharper disable once CheckNamespace
public class EnemyMelee : MonoBehaviour
{
    [SerializeField] private int _routine;
    [SerializeField] private float _timer, _grade, _speed;
    [SerializeField] private Quaternion _angle;
    [SerializeField] private Rigidbody _rb; // this is never used, safe to remove?
    [SerializeField] private GameObject _target;
    [SerializeField] private NavMeshAgent _agent; // this is never used, safe to remove?

    private void Start()
    {
        _target = GameObject.Find("Player"); // TODO refactor this
    }

    private void Update()
    {
        EnemyStates();
    }

    private void EnemyStates()
    {
        if (Vector3.Distance(transform.position, _target.transform.position) > 15) //si el jugador se encuentra a mas de 15 metros, patrulla.
        {
            _timer += 1 * Time.deltaTime;
            if (_timer >= 4)
            {
                _routine = Random.Range(0, 2);
                _timer = 0;
            }
            switch (_routine)
            {
                case 0:

                    break;
                case 1:
                    _grade = Random.Range(0, 360);
                    _angle = Quaternion.Euler(0, _grade, 0);
                    _routine++;
                    break;
                case 2:
                    transform.rotation = Quaternion.RotateTowards(transform.rotation, _angle, 0.5f);
                    transform.Translate(Vector3.forward * (1 * Time.deltaTime));

                    break;
            }
        }
        else Chase();
    }

    private void Chase()
    {
        var lookPos = _target.transform.position - transform.position;
        lookPos.y = 0;
        var rotation = Quaternion.LookRotation(lookPos);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, rotation, 2);
        transform.Translate(Vector3.forward * (_speed * 2 * Time.deltaTime));
        if (Vector3.Distance(transform.position, _target.transform.position) < 5); // TODO figure out what's this supposed to do
    }    
}