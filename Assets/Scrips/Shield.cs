using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour
{
    public float followSpeed = 5f;       
    int curShield = default;
    public int maxShield = default;
    public SpriteRenderer mySprite = default;
    public AudioClip[] sonidos;
    private AudioSource audioSource;   
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        curShield = maxShield;
        mySprite = GetComponent<SpriteRenderer>();      
    }
    public void TomarDaño(int dañoE)//DAÑO AL ESCUDO
    {
        curShield -= dañoE;        
        // dialogo = Dañorecibido;
        //ShowText();
    }
   
    void Update()
    {       
        changeColor();
        DestroyShield();
    }
    void DestroyShield()
    {
        if (curShield <= 0)
        {            
            this.gameObject.SetActive(false);           
            curShield = maxShield;
            mySprite.color = Color.white;
        }       
    }
    public void DañoEscudo(int dañoE)//DAÑO AL ESCUDO
    {
        curShield -= dañoE;
        ReproducirSonidoAleatorio();
        // dialogo = Dañorecibido;
        //ShowText();
    }
    public void ReproducirSonidoAleatorio()
    {
        if (sonidos.Length > 0)
        {
            // Seleccionar sonido
            int indiceAleatorio = Random.Range(0, sonidos.Length);

            // Asignar el  audio 
            audioSource.clip = sonidos[indiceAleatorio];

            // Reproducir  audio
            audioSource.Play();
        }
        else
        {
            Debug.Log("No hay sonidos");
        }
    }
    void changeColor()
    {
        if (curShield < maxShield / 2)
        {
            mySprite.color = Color.yellow;
        }
        if (curShield < maxShield / 3)
        {
            mySprite.color = Color.red;
        }
    }
}
