using UnityEngine;
using Weapons;

// ReSharper disable once CheckNamespace
[RequireComponent(typeof(Movement3D))]
[RequireComponent(typeof(WeaponChanger))]
public class InputController : MonoBehaviour
{
    public Movement3D movement3D;
    public WeaponChanger weaponChanger;

    private RangedWeapon _rangedWeapon;

    private void Awake()
    {
        if (!movement3D) movement3D = GetComponent<Movement3D>();
        if (!weaponChanger) weaponChanger = GetComponent<WeaponChanger>();
    }

    private void Update()
    {
        MouseInput();
        SprintInput();
        MovementInput();
        
        ChangeWeaponInput();
        ReloadWeaponInput();

        if (Input.GetMouseButtonDown(0) && !Cursor.visible)
        {
            weaponChanger.weapons[weaponChanger.selectedWeapon].Attack();
        }
    }

    private void ChangeWeaponInput()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            _rangedWeapon = null;
            StartCoroutine(weaponChanger.CycleWeaponBackward());
        }
        else if (Input.GetKeyDown(KeyCode.E))
        {
            _rangedWeapon = null;
            StartCoroutine(weaponChanger.CycleWeaponForward());
        }
    }
    
    private void ReloadWeaponInput()
    {
        if (!Input.GetKeyDown(KeyCode.R)) return;

        if (_rangedWeapon)
        {
            _rangedWeapon.Reload();
        }
        else if (weaponChanger.weapons[weaponChanger.selectedWeapon].TryGetComponent(out RangedWeapon rangedWeapon))
        {
            _rangedWeapon = rangedWeapon;
            _rangedWeapon.Reload();
        }
    }

    private void SprintInput()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            movement3D.Sprint(true);
        }
        else if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            movement3D.Sprint(false);
        }
    }

    private void MovementInput()
    {
        var myTransform = transform;
        var forwardDirection = myTransform.forward * Input.GetAxisRaw("Vertical");
        var lateralDirection = myTransform.right * Input.GetAxisRaw("Horizontal");
        
        movement3D.Direction = (forwardDirection + lateralDirection).normalized;
    }

    private void MouseInput()
    {
        var mouseX = Input.GetAxis("Mouse X");
        var mouseY = Input.GetAxis("Mouse Y");
        
        movement3D.MouseLook(mouseX, mouseY);
    }
}