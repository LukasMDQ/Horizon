using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMele : MonoBehaviour
{
    [SerializeField] int _rutine;
    [SerializeField] float _timer, _grade, _speed;
    [SerializeField] Quaternion _angle;
    [SerializeField] Rigidbody _rb;
    [SerializeField] GameObject _target;
    [SerializeField] NavMeshAgent _agent;   

    private void Start()
    {
        _target = GameObject.Find("Player");
    }
    void Update()
    {
        EnemyStates();
    }
    public void EnemyStates()
    {
        if (Vector3.Distance(transform.position, _target.transform.position) > 15)//si el jugador se encuentra a mas de 15 metros, patrulla.
        {
            _timer += 1 * Time.deltaTime;
            if (_timer >= 4)
            {
                _rutine = Random.Range(0, 2);
                _timer = 0;
            }
            switch (_rutine)
            {
                case 0:

                    break;
                case 1:
                    _grade = Random.Range(0, 360);
                    _angle = Quaternion.Euler(0, _grade, 0);
                    _rutine++;
                    break;
                case 2:
                    transform.rotation = Quaternion.RotateTowards(transform.rotation, _angle, 0.5f);
                    transform.Translate(Vector3.forward * 1 * Time.deltaTime);

                    break;
            }
        }
        else Perseguir();

    }
    void Perseguir()
    {
        var lookPos = _target.transform.position - transform.position;
        lookPos.y = 0;
        var rotation = Quaternion.LookRotation(lookPos);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, rotation, 2);
        transform.Translate(Vector3.forward * _speed * 2 * Time.deltaTime);
        if (Vector3.Distance(transform.position, _target.transform.position) < 5) ;
    }    
}
