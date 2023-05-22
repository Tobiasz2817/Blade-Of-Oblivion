using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class RollingCharacterAnimation : MonoBehaviour
{
    [SerializeField] private Animator characterAnim;
    [SerializeField] private ListenerLastPressedVectorBinds listenerBinds;
    [SerializeField] private InputActionReference rollTriggerAction;

    private bool canRolling = true;

    private void Start() {
        rollTriggerAction.action.Enable();
    }
    
    private void OnEnable() {
        rollTriggerAction.action.performed += Roll;
    }
    
    private void OnDisable() {
        rollTriggerAction.action.performed -= Roll;
    }

    private void Roll(InputAction.CallbackContext obj) {
        if (!listenerBinds.IsFindingBind() || !canRolling) return;

        var lastPressBind = GetDirectionByEnum(listenerBinds.GetDirectionBind());
        
        characterAnim.SetInteger("RollDirection",lastPressBind);
        characterAnim.SetTrigger("Roll");
    }

    private int GetDirectionByEnum(ListenerLastPressedVectorBinds.BindDirection inputDirection) {
        switch (inputDirection.bindValue) {
            case -1: {
                if (inputDirection.side == Side.X)
                    return (int)RollingStates.Left;
                
                return (int)RollingStates.Backward;
            }
            case 1: {
                if (inputDirection.side == Side.X)
                    return (int)RollingStates.Right;
                
                return (int)RollingStates.Forward;
            }
        }

        return 99;
    }
    
    public void ChangeStateRolling(bool canRolling_) {
        canRolling = canRolling_;
    }
}

public enum RollingStates
{
    Backward = -1,
    Forward = 1,
    Left = -2,
    Right = 2
}