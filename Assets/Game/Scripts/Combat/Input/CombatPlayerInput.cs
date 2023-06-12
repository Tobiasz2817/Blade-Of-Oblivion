using UnityEngine;
using UnityEngine.InputSystem;

public class CombatPlayerInput : CombatInput
{
    [SerializeField] protected InputActionReference bindAttack;

    protected virtual void OnEnable() {
        bindAttack.action.performed += InvokeAttackBind;
        bindAttack.action.canceled += InvokeAttackBind;
        bindAttack.action.Enable();
    }

    protected virtual void OnDisable() {
        bindAttack.action.performed -= InvokeAttackBind;
        bindAttack.action.canceled -= InvokeAttackBind;
        bindAttack.action.Disable();
    }
    
    private void InvokeAttackBind(InputAction.CallbackContext callbackContext) {
        if (callbackContext.performed) 
            OnPress?.Invoke();
        else if (callbackContext.canceled)
            OnRealesed?.Invoke();
    }
}
