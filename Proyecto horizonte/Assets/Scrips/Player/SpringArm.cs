using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpringArm : MonoBehaviour
{
    [SerializeField]
    [Range(0, 10)]
    float _mouseSensivityX = 500;

    [SerializeField]
    [Range(0, 10)]
    float _mouseSensivityY = 500;

    [SerializeField]
    float _mouseYRot;

    [SerializeField]
    float _mouseXRot;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        _mouseXRot += Input.GetAxisRaw("Mouse X") * _mouseSensivityX * Time.deltaTime;
        _mouseYRot += Input.GetAxisRaw("Mouse Y") * _mouseSensivityY * Time.deltaTime;

        transform.rotation = Quaternion.Euler(-_mouseYRot, _mouseXRot, 0);
    }
}
