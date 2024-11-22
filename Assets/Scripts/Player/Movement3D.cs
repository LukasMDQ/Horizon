using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
// ReSharper disable once CheckNamespace
//TP2- Hernandez Lucas
public class Movement3D : MonoBehaviour
{
    private Rigidbody _rigidBody;
    private Vector3   _direction;
    
    public static Transform playerTransform;
    
    public  float mouseSensitivity;
    private float _mouseRotationX, _mouseRotationY;
    
    // ReSharper disable once InconsistentNaming
    [SerializeField] private float _speed;
    private float _walkSpeed;
    private float _sprintSpeed;
    public Stats Stats;

    private void Awake()
    {
        #region HideCursor
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        #endregion
        
        if (!_rigidBody) _rigidBody = GetComponent<Rigidbody>();
        playerTransform = transform;
    }
    
    private void Start()
    {
        _walkSpeed = _speed;
        _sprintSpeed = _speed*2;
    }
    private void Update()
    {
        if ( Stats.stamina <=1)
        {
            _speed = _walkSpeed;
        }
    }

    private void FixedUpdate()
    {
        // Keep the vertical velocity component so as to not overwrite gravity
        var currentVelocity = _rigidBody.velocity;

        // We only modify the X and Y components for horizontal movement
        _rigidBody.velocity = new Vector3(_direction.x * _speed, currentVelocity.y, _direction.z * _speed);
    }

    public Vector3 Direction
    {
        set => _direction = value;
    }

    public void MouseLook(float mouseX, float mouseY)
    {
        _mouseRotationX -= mouseY * mouseSensitivity * Time.deltaTime;
        _mouseRotationX = Mathf.Clamp(_mouseRotationX, -70, 70);
        _mouseRotationY += mouseX * mouseSensitivity * Time.deltaTime;
        
        _rigidBody.MoveRotation(Quaternion.Euler(_mouseRotationX, _mouseRotationY, 0));
    }

    public void Sprint(bool isPressingButton)
    {
        _speed = isPressingButton ? _sprintSpeed : _walkSpeed;
    }
    
    public void Slow (float slowValue) // REDUCE SPEED VALUE
    {
        _speed -= slowValue; // TODO probably refactor this later, speed never returns to normal value so player it's slowed permanently
    }
}