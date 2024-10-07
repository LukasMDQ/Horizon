using System.Collections.Generic;
using UnityEngine;
// ReSharper disable InconsistentNaming
[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(BoxCollider))]

public class FPSControl : MonoBehaviour
{
    [SerializeField] private Rigidbody _rigidBody;
    [SerializeField] private BoxCollider _boxCollider;
    [SerializeField] private float _speed, _mouseSens, _rotationSpeed, _jumpForce;
    private float _xRotation, _yRotation;
    public Transform playerBody;
    [SerializeField] private AudioSource _AudSource;
    [SerializeField] private bool _wantsToJump;

    private float _verticalInput;
    private float _horizontalInput;

    private void Awake()
    {
        if (!_rigidBody)   _rigidBody = GetComponent<Rigidbody>();
        if (!_boxCollider) _boxCollider = GetComponent<BoxCollider>();
        HideCursor();
    }

    private void Update()
    {
        _verticalInput   = Input.GetAxis("Vertical");
        _horizontalInput = Input.GetAxis("Horizontal");
        
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
        
        _rigidBody.MoveRotation(Quaternion.Euler(_xRotation, _yRotation, 0));
        
        var myTransform = transform;
        var moveDirection = _verticalInput * myTransform.forward + _horizontalInput * myTransform.right;
        _rigidBody.MovePosition(myTransform.position + moveDirection.normalized * (Time.fixedDeltaTime * _speed));
    }

    private void Jump()//----Salto
    {
        _rigidBody.AddForce(transform.root.up * _jumpForce, ForceMode.Impulse); // Changed it so that even if you are looking at different angles you will always jump up relative to the world
    }

    private void HideCursor()//----------------BLOQUEAR CURSOR
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void ShowCursor()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    private void MouseLook()
    {
        float mouseX = Input.GetAxis("Mouse X") * _mouseSens * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * _mouseSens * Time.deltaTime;
        
        _xRotation -= mouseY;
        _xRotation = Mathf.Clamp(_xRotation, -70, 70);
        _yRotation += mouseX;
    }
}