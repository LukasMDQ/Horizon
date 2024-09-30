using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class Skeleton : MonoBehaviour
{
    //Stats   
    public float speed = 0.5f;
    //Animations
    [SerializeField] bool _pasive = true;
    public bool onAttack;
    public bool attack;
    public Animator anim;
   
    //Navegation
    public NavMeshAgent navAgent;
    public Quaternion angle;
    public float grade;
    public int rutine;
    public float crono;
    public float attackDistance;
    public float visionRange;
    //Physics
    public Rigidbody rb;
    //targets
    public Transform target;
    private void Start()
    {
        target = transform.Find("Player");
        anim = GetComponent<Animator>();          
        
    }
    private void Update()
    {
        Enemy_Status();
    }

    public virtual void Enemy_Status()//"virtual" permite sobreescribir el metodo.
    {
        if (Vector3.Distance(transform.position, target.transform.position) > visionRange)//si el objetivo se encuentra a mas de 10 metros se movera erraticamente.
        {
           /* if (!_pasive)//si es atacado, perseguira al jugador
            {
                navAgent.enabled= false;
                var lookPos = target.transform.position - transform.position;
                lookPos.y = 0;
                var rotation = Quaternion.LookRotation(lookPos);
                transform.rotation = Quaternion.RotateTowards(transform.rotation, rotation, 2);
                anim.SetBool("walk", false);
                anim.SetBool("run", true);
                transform.Translate(Vector3.forward * 2 * Time.deltaTime);//* speed

                if (Vector3.Distance(transform.position, target.transform.position) <= 1)//si el objetivo se encuentra a menos de 1 metros lo atacara.
                {
                    anim.SetBool("walk", false);
                    anim.SetBool("run", false);

                    anim.SetBool("attackM", true);
                    onAttack = true;
                }

            }*/
           //---------------------------------------------------------------------------------------------------------
            anim.SetBool("run", false);//Movimiento erratico
            crono += 1 * Time.deltaTime;
            if (crono >= 4)
            {
                rutine = Random.Range(0, 2);
                crono = 0;
            }
            switch (rutine)
            {
                case 0:
                    anim.SetBool("walk", false);
                    break;
                case 1:
                    grade = Random.Range(0, 360);
                    angle = Quaternion.Euler(0, grade, 0);
                    rutine++;
                    break;
                case 2:
                    transform.rotation = Quaternion.RotateTowards(transform.rotation, angle, 0.5f);
                    transform.Translate(Vector3.forward * speed * Time.deltaTime);
                    anim.SetBool("walk", true);
                    break;
            }
        }
            else//ve al objetivo y lo persigue.-------------------------------------------------------------------------
            {
             //si esta mas de un metro y no esta atacando lo perseguira .
           
                var lookPos = target.transform.position - transform.position;
                lookPos.y = 0;
                var rotation = Quaternion.LookRotation(lookPos);

                navAgent.enabled = true;
                navAgent.SetDestination(target.transform.position);

                if(Vector3.Distance(transform.position,target.transform.position) > attackDistance && !onAttack)
                {
                    anim.SetBool("walk", false);
                    anim.SetBool("run", true);
                }
                              
                else
                {
                    if(!onAttack)
                    {
                        transform.rotation = Quaternion.RotateTowards(transform.rotation,rotation, 1);
                        anim.SetBool("walk", false);
                         anim.SetBool("run", false);
                    }
                    

                     anim.SetBool("attackM", true);
                     onAttack = true;
                }
            }
            if(onAttack) 
            {
                navAgent.enabled = true;
            }
    }
    public void Final_Ani()
    {
        if(Vector3.Distance(transform.position,target.transform.position) > attackDistance +0.2f)
        {
            anim.SetBool("attackM", false);
        }
        
        onAttack = false;
    }

}

        