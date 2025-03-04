using System.Collections;
using UnityEngine;

public class ControlObjetos : MonoBehaviour
{
    public GameObject objeto2; // Objeto que se activará y desactivará
    public float intervalo = 1f; // Intervalo de tiempo

    private Coroutine toggleCoroutine; // Referencia a la corrutina en ejecución

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // Verifica que sea el Player
        {
            if (gameObject.name == "Objeto1") // Si es Objeto1, inicia la corrutina
            {
                if (toggleCoroutine == null)
                {
                    toggleCoroutine = StartCoroutine(ToggleObjeto2());
                }
            }
            else if (gameObject.name == "Objeto3") // Si es Objeto3, detiene la corrutina
            {
                if (toggleCoroutine != null)
                {
                    StopCoroutine(toggleCoroutine);
                    toggleCoroutine = null;
                }
            }
        }
    }

    private IEnumerator ToggleObjeto2()
    {
        while (true)
        {
            objeto2.SetActive(!objeto2.activeSelf);
            yield return new WaitForSeconds(intervalo);
        }
    }
}
