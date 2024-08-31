using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

public class BalaEnemigo : MonoBehaviour
{
    Rigidbody rb;
    [SerializeField] int _lifeTime = default;
    GameObject player;
    [SerializeField] int force;
    public int damageB = default;
    public GameObject explosion;

    void Start()
    {
        Destroy(gameObject, _lifeTime);

        rb = GetComponent<Rigidbody>();
        player = GameObject.FindGameObjectWithTag("Player");
        Vector3 direction = player.transform.position - transform.position;
        rb.velocity = new Vector2(direction.x, direction.y).normalized * force;
        float rot = Mathf.Atan2(-direction.y, -direction.x) * Mathf.Rad2Deg;// angulo de rotación para que el objeto mire hacia la direccion del jugador.
        transform.rotation = Quaternion.Euler(0, 0, rot + 90);
    }
    /*
    private void OnTriggerEnter(Collider2D other)
    {
        
        if (other.CompareTag("Limits")) Destroy(gameObject);
        if (other.TryGetComponent(out Stats stats))//DAÑO AL JUGADOR        
        {
           // stats.TomarDaño(damageB);
            destruct();
        }
        // if (other.CompareTag("PlyerProyectile")) Destroy(gameObject);


        if (other.TryGetComponent(out Shield shield))//DAÑO AL ESCUDO
        {
            shield.DañoEscudo(damageB);
            destruct();
        }

        //if (other.CompareTag("PlyerProyectile")) Destroy(gameObject);
        void destruct()
        {
            Instantiate(explosion, transform.position, transform.rotation);
            Destroy(gameObject);
        }
     }
    */

}
