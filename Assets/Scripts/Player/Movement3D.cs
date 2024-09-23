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
    Vector3 _direction;

    [SerializeField]
    float _speed;

    Rigidbody _rigidbody;
    BoxCollider _boxCollider;

    [SerializeField]
    ForceMode _forceMode;

    void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _boxCollider = GetComponent<BoxCollider>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float VerticalAxis = Input.GetAxis("Vertical");
        float HorizontalAxis = Input.GetAxis("Horizontal");
        float HeightAxis = Input.GetAxis("Height");

        Vector3 forwardDirection = transform.forward * VerticalAxis;
        Vector3 rightDirection = transform.right * HorizontalAxis;
        Vector3 verticalDirection = transform.up * HeightAxis;

        _direction = forwardDirection + rightDirection + verticalDirection;
        _direction.Normalize();
        //transform.position += _direction * _speed * Time.deltaTime;
    }

    void FixedUpdate()
    {
        //_rigidbody.MovePosition(transform.position + (_speed * _direction * Time.fixedDeltaTime));
        //_rigidbody.AddForce(_speed * _direction, _forceMode);

        _rigidbody.velocity = _direction * _speed;
    }
}
