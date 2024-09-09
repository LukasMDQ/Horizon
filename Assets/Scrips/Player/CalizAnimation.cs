using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CalizAnimation : MonoBehaviour
{
    Animator caliz;
    void Start()
    {
        caliz = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1) || Input.GetKeyDown(KeyCode.Alpha2) || Input.GetKeyDown(KeyCode.Alpha3)){
            caliz.SetTrigger("Trigger");
        }
    }


}
