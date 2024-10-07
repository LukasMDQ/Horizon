using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Security.Cryptography;
using System.Threading;
using UnityEngine;


[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(BoxCollider))]

public class Movement3D : MonoBehaviour
{
    public float mouseSens;
    float _xRotation, _yRotation;
    Vector3 _direction;
    public static Transform playerTransform;

    [SerializeField]
    float _speed;
    float _sprintSpeed;
    float _walkSpeed;

    Rigidbody _rigidBody;
    BoxCollider _boxCollider;

    [SerializeField]
    ForceMode _forceMode;

    void Awake()
    {
        HideCursor();       
        _rigidBody = GetComponent<Rigidbody>();
        _boxCollider = GetComponent<BoxCollider>();
        playerTransform = transform;
    }

    // Start is called before the first frame update
    void Start()
    {
        _walkSpeed = _speed;
        _sprintSpeed = _speed*2;
    }

    // Update is called once per frame
    void Update()
    {
        sprint();
        MouseLook();
        _rigidBody.MoveRotation(Quaternion.Euler(_xRotation, _yRotation, 0));
        float VerticalAxis = Input.GetAxis("Vertical");
        float HorizontalAxis = Input.GetAxis("Horizontal");
        //float HeightAxis = Input.GetAxis("Height");

        Vector3 forwardDirection = transform.forward * VerticalAxis;
        Vector3 rightDirection = transform.right * HorizontalAxis;
        //Vector3 verticalDirection = transform.up * HeightAxis;

        _direction = forwardDirection + rightDirection;//+ verticalDirection
        _direction.Normalize();
        //transform.position += _direction * _speed * Time.deltaTime;
      
    }

    void FixedUpdate()
    {
        //_rigidbody.MovePosition(transform.position + (_speed * _direction * Time.fixedDeltaTime));
        //_rigidbody.AddForce(_speed * _direction, _forceMode);

        _rigidBody.velocity = _direction * _speed;
    }
    private void MouseLook()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSens * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSens * Time.deltaTime;

        _xRotation -= mouseY;
        _xRotation = Mathf.Clamp(_xRotation, -70, 70);
        _yRotation += mouseX;
    }
    private void HideCursor()//----------------BLOQUEAR CURSOR
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
    void sprint()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            // Cambiamos la variable al nuevo valor
            _speed = _sprintSpeed;
        }
        else
        {
            // Volvemos la variable al valor inicial
            _speed = _walkSpeed;
        }        
    }
}
