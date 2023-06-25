using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CombatRollingCustomInput : JumpInDirectionInput
{
    [System.Serializable]
    public class RollingDependencies
    {
        public SingleAttack rollingAnimSide;
        public RollingStates rollingState;
    }

    [SerializeField] private InputActionReference rollingAction;
    [SerializeField] private ListenerLastPressedVectorBinds listenerBinds;
    [SerializeField] private List<RollingDependencies> rollingDependenciesList = new List<RollingDependencies>();

    private bool isRolling = false;
    
    private void OnEnable() {
        rollingAction.action.performed += StartRollingInput;
        rollingAction.action.Enable();
        rollingDependenciesList.ForEach((rollingDependencies) => {
            rollingDependencies.rollingAnimSide.OnStartAnimAttack += StartRolling;
            rollingDependencies.rollingAnimSide.OnExecuteAnimAttack += EndRolling;
        });
    }
    
    private void OnDisable() {
        rollingAction.action.performed -= StartRollingInput;
        rollingAction.action.Disable();
        rollingDependenciesList.ForEach((rollingDependencies) => {
            rollingDependencies.rollingAnimSide.OnStartAnimAttack -= StartRolling;
            rollingDependencies.rollingAnimSide.OnExecuteAnimAttack -= EndRolling;
        });
    }
    
    private void StartRollingInput(InputAction.CallbackContext obj) {
        if (isRolling) return;
        
        if (!listenerBinds.IsFindingBind()) return;
        var bindDirection = listenerBinds.GetDirectionBind();
        var attack = GetAttackByRollingStates(GetDirectionByEnum(bindDirection));
        attack.OnPressedBind?.Invoke(attack);
    }
    
    
    private void StartRolling(Attack obj) {
        OnStartJump?.Invoke(GetDirectionByLastInput());
        isRolling = true;
    }
    
    private void EndRolling(Attack attack) {
        OnEndJump?.Invoke();
        isRolling = false;
    }

    private Attack GetAttackByRollingStates(RollingStates rollingState) {
        foreach (var rollingDependencies in rollingDependenciesList) {
            if (rollingDependencies.rollingState == rollingState)
                return rollingDependencies.rollingAnimSide;
        }

        return null;
    }
    
    private RollingStates GetDirectionByEnum(ListenerLastPressedVectorBinds.BindDirection inputDirection) {
        switch (inputDirection.bindValue) {
            case -1: {
                if (inputDirection.side == Side.X)
                    return RollingStates.Left;
                
                return RollingStates.Backward;
            }
            case 1: {
                if (inputDirection.side == Side.X)
                    return RollingStates.Right;
            }
                break;
        }

        return RollingStates.Forward;
    }
    
    private Vector3 GetDirectionByLastInput() {
        var directionBind = listenerBinds.GetDirectionBind();
        if (directionBind.side == Side.X)
            return transform.right * directionBind.bindValue;

        return transform.forward * directionBind.bindValue;
    }
}

public enum RollingStates
{
    Backward = -1,
    Forward = 1,
    Left = -2,
    Right = 2
}