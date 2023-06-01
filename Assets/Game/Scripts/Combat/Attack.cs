using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

public abstract class Attack : MonoBehaviour
{
    public Action<Attack> OnPressedBind;
    public Action<Attack> OnRealesedBind;
    protected bool isAnimating = false;

    [SerializeField] protected InputActionReference bindAttack;
    [SerializeField] protected Animator animator;

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

    protected virtual void InvokeAttackBind(InputAction.CallbackContext callbackContext) {
        if (callbackContext.performed)
            OnPressedBind?.Invoke(this);
        else if (callbackContext.canceled)
            OnRealesedBind?.Invoke(this);
    }

    public abstract void MakingAttack();
    public abstract bool IsAnimating();
    public abstract bool BlockingMovement();

    protected void SpeedAnimation(float increaseSpeedBy, string nameParameter) {
        var animInfo = animator.GetCurrentAnimatorStateInfo(0);
        var speed = animInfo.speed + (animInfo.speed * increaseSpeedBy); 
        animator.SetFloat(nameParameter,speed);
    }

    protected void BackSpeed(float oldValue, string nameParameter) {
        animator.SetFloat(nameParameter,oldValue);
    }
    
    protected IEnumerator StopBeforeSomeTime(float breakTimeCombo) {
        yield return new WaitUntil(() => {
            var animInfo = animator.GetCurrentAnimatorStateInfo(0);
            var stop = animInfo.length - (animInfo.length * breakTimeCombo);
            var currentAnimFrame = animInfo.normalizedTime * animInfo.length;
            return currentAnimFrame > stop;
        });
    }
    
    protected IEnumerator WaitForEndAnimation(string nameAnimation) {
        yield return new WaitUntil(() => !animator.GetCurrentAnimatorStateInfo(0).IsName(nameAnimation));
    }
}

public class CountMousePress
{
    private Dictionary<int, float> pressTimer = new Dictionary<int, float>();

    private int currentTimePressed = 0;
    
    public void IncrementPressing() {
        currentTimePressed++;
        
        pressTimer.Add(currentTimePressed,Time.time);
    }

    public void ResetTime() {
        pressTimer.Clear();
        currentTimePressed = 0;
    }

    public bool ButtonWasPressedLastTime(float duration) {
        var currentTime = Time.time;

        if (currentTime - pressTimer.Values.Last() < duration)
            return true;
        
        return false;
    }
}
