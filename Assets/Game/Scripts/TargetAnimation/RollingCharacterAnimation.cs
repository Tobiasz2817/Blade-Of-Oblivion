using UnityEngine;
using UnityEngine.InputSystem;

public class RollingCharacterAnimation : MonoBehaviour
{
    [SerializeField] private Animator characterAnim;
    [SerializeField] private ListenerLastPressedVectorBinds listenerBinds;
    [SerializeField] private InputActionReference rollTriggerAction;
    [SerializeField] private CombatManager combat;

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
        if (!listenerBinds.IsFindingBind() || !canRolling || combat.IsSomeAttackInvoke()) return;

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

