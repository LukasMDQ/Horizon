using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(BoxCollider))]

public class FPSControl : MonoBehaviour
{
    Rigidbody _rigidbody;
    BoxCollider _boxCollider;
    [SerializeField] float _Speed, _mouseSens, _rotationSpeed, _jumpForce = default;
    float _xRotation, _yRotation;
    public Transform playerBody;    
    [SerializeField] AudioSource _AudSource;
    [SerializeField] bool _wantsToJump;
    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _boxCollider = GetComponent<BoxCollider>();
    }



    void Update()
    {
        Move();
        HideCursor();
        MouseLook();
    }
    void FixedUpdate()
    {
        if (_wantsToJump)
        {
            Debug.Log("Trying to jump");
            Jump();
            _wantsToJump = false;
        }
    }


    void Move()
    {
        float hor = Input.GetAxis("Horizontal");
        float ver = Input.GetAxis("Vertical");
        Vector3 inputPlayer = new Vector3(hor, 0, ver);
        transform.Translate(new Vector3(hor, 0, ver) * _Speed * Time.deltaTime);        
    }
    void Jump()//----Salto
    {
        _rigidbody.AddForce(transform.up * _jumpForce, ForceMode.Impulse);
    }

    public void HideCursor()//----------------BLOQUEAR CURSOR
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
    void MouseLook()
    {
        float mouseX = Input.GetAxis("Mouse X") * _mouseSens * Time.deltaTime;

        float mouseY = Input.GetAxis("Mouse Y") * _mouseSens * Time.deltaTime;


        _xRotation -= mouseY;

        _xRotation = Mathf.Clamp(_xRotation, -70, 70);



        _yRotation += mouseX;



        transform.localRotation = Quaternion.Euler(_xRotation, _yRotation, 0);

    }
}
