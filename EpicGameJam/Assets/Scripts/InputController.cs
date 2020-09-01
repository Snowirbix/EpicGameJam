using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(MovementController))]
[RequireComponent(typeof(WeaponController  ))]
public class InputController : MonoBehaviour
{
    public enum MouseButton
    {
        Left    = 0,
        Middle  = 1,
        Right   = 2,
    }

    protected MovementController moveCtrl;
    protected WeaponController   weaponCtrl;
    protected PlayerControls ctrls;

    private void Awake ()
    {
        moveCtrl   = GetComponent<MovementController>();
        weaponCtrl = GetComponent<  WeaponController>();

        ctrls = new PlayerControls();

        //ctrls.Grounded.Attack.started  += Attack_started;
        //ctrls.Grounded.Attack.canceled += Attack_canceled;
    }

    private void Attack_started (InputAction.CallbackContext obj)
    {
        weaponCtrl.Charge();
    }

    private void Attack_canceled(InputAction.CallbackContext obj)
    {
        weaponCtrl.Fire();
    }

    private void OnEnable ()
    {
        ctrls.Grounded.Enable();
    }

    private void OnDisable ()
    {
        ctrls.Grounded.Disable();
    }

    private void Update ()
    {
        Vector2 input = ctrls.Grounded.Move.ReadValue<Vector2>();
        Vector3 direction = input.Dir3();
        direction = transform.localRotation * direction;
        direction.Normalize();

        moveCtrl.Move(direction.XZ());
    }
}
