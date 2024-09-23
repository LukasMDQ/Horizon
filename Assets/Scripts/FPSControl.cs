using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(BoxCollider))]

public class FPSControl : MonoBehaviour
{
    [SerializeField] private Rigidbody _rigidbody;
    [SerializeField] private BoxCollider _boxCollider;
    [SerializeField] private float _speed, _mouseSens, _rotationSpeed, _jumpForce;
    private float _xRotation, _yRotation;
    public Transform playerBody;    
    [SerializeField] private AudioSource _AudSource;
    [SerializeField] private bool _wantsToJump;
    private void Awake()
    {
        if (!_rigidbody)   _rigidbody   = GetComponent<Rigidbody>();
        if (!_boxCollider) _boxCollider = GetComponent<BoxCollider>();
    }

    private void Update()
    {
        Move();
        HideCursor();
        MouseLook();
    }

    private void FixedUpdate()
    {
        if (_wantsToJump)
        {
            Debug.Log("Trying to jump");
            Jump();
            _wantsToJump = false;
        }
    }

    private void Move()
    {
        float hor = Input.GetAxis("Horizontal");
        float ver = Input.GetAxis("Vertical");
        Vector3 inputPlayer = new Vector3(hor, 0, ver);
        transform.Translate(inputPlayer * (_speed * Time.deltaTime));        
    }

    private void Jump()//----Salto
    {
        _rigidbody.AddForce(transform.up * _jumpForce, ForceMode.Impulse);
    }

    private void HideCursor()//----------------BLOQUEAR CURSOR
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void MouseLook()
    {
        float mouseX = Input.GetAxis("Mouse X") * _mouseSens * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * _mouseSens * Time.deltaTime;
        
        _xRotation -= mouseY;
        _xRotation = Mathf.Clamp(_xRotation, -70, 70);
        _yRotation += mouseX;
        
        transform.localRotation = Quaternion.Euler(_xRotation, _yRotation, 0);
    }
}