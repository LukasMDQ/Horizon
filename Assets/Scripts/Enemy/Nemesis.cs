using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Serialization;

public class Nemesis : MonoBehaviour
{
    public int rutina;
    public float cronometro;
    public Quaternion angulo;
    public float grado;
    public Rigidbody rb;
    public GameObject target;
    public NavMeshAgent agente;
    public float speed;
    public EnemyShot enemyShot;
    public GameObject balaEnemiga;
    //----Conducta
    public bool hostil;
    //Disparo
    [SerializeField] private float _fireRate = 2;
    [SerializeField] private float _timer = 0;
    public GameObject spawnPoint;

    private void Start()
    {   
        hostil= false;
        target = GameObject.Find("Player"); // TODO refactor this
    }

    private void Update()
    {
        Comportamiento_Enemigo();        
    }
    public void Agresion()
    {
        hostil = true;
    }
    public void Comportamiento_Enemigo()
    {
        if (Vector3.Distance(transform.position, target.transform.position) > 15 && !hostil)//si el jugador se encuentra a mas de 15 metros y no este hostil, patrulla.
        {
            cronometro += 1 * Time.deltaTime;
            if (cronometro >= 4)
            {
                rutina = Random.Range(0, 2);
                cronometro = 0;
            }
            switch (rutina)
            {
                case 0:

                    break;
                case 1:
                    grado = Random.Range(0, 360);
                    angulo = Quaternion.Euler(0, grado, 0);
                    rutina++;
                    break;
                case 2:
                    transform.rotation = Quaternion.RotateTowards(transform.rotation, angulo, 0.5f);
                    transform.Translate(Vector3.forward * (1 * Time.deltaTime));

                    break;
            }
        }
        else Perseguir();
    }

    private void Perseguir()
    {
        if (Vector3.Distance(transform.position, target.transform.position) > 10 || hostil)// mientras el jugador este a mas de 10 metros lo seeguira, sino dispara.
        {
            speed = 2;
            var lookPos = target.transform.position - transform.position;
            lookPos.y = 0;
            var rotation = Quaternion.LookRotation(lookPos);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, rotation, 2);
            transform.Translate(Vector3.forward * (speed * 2 * Time.deltaTime));
            if (Vector3.Distance(transform.position, target.transform.position) < 5);
        }
        else Shoot();
    }

    private void Shoot()
    {
        var lookPos = target.transform.position - transform.position;
        lookPos.y = 0;
        var rotation = Quaternion.LookRotation(lookPos);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, rotation, 2);
        if (_timer <= _fireRate)//timer
        {
            _timer += Time.deltaTime;
        }
        else
        {
            _timer = 0;

            Instantiate(balaEnemiga, spawnPoint.transform.position, Quaternion.identity);
        }
    }
}