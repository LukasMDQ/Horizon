using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(CapsuleCollider))]
public class Player : MonoBehaviour
{
    Vector3 _direction;

    [SerializeField]
    float _speed;

    Rigidbody _rigidbody;
    CapsuleCollider _capsuleCollider;

    [SerializeField]
    ForceMode _forceMode;

    [SerializeField]
    float _jumpForce;

    [SerializeField]
    bool _wantsToJump;

    Transform _cameraTransform;

    void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _capsuleCollider = GetComponent<CapsuleCollider>();
        _cameraTransform = GetComponentInChildren<Camera>().transform;
    }

    // Update is called once per frame
    void Update()
    {
        float VerticalAxis = Input.GetAxis("Vertical");
        float HorizontalAxis = Input.GetAxis("Horizontal");        

        Vector3 forwardDirection = _cameraTransform.forward * VerticalAxis;
        Vector3 rightDirection = transform.right * HorizontalAxis;

        _direction = forwardDirection + rightDirection;
        _direction.Normalize();

        if (!_wantsToJump)
            _wantsToJump = Input.GetButtonDown("Jump");
    }

    void FixedUpdate()
    {
        _rigidbody.MovePosition(transform.position + (_speed * _direction * Time.fixedDeltaTime));

        //_rigidbody.AddForce(_speed * _direction, _forceMode);
        //_rigidbody.velocity = _direction * _speed;

        if (_wantsToJump)
        {
            Debug.Log("Trying to jump");
            Jump();
            _wantsToJump = false;
        }
    }

    void Jump()
    {
        _rigidbody.AddForce(transform.up * _jumpForce, ForceMode.Impulse);
    }
}
