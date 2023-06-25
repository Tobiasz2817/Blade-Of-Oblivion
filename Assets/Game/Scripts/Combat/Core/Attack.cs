using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public abstract class Attack : MonoBehaviour
{
    public Action<Attack> OnPressedBind;
    public Action<Attack> OnReleasedBind;
    public Action<Attack> OnTriggerEvent;
    public Action<Attack> OnStartAnimAttack;
    public Action<Attack> OnExecuteAnimAttack;
    protected bool isAnimating = false;

    [SerializeField] protected CombatInput combatInput;
    [SerializeField] protected Animator animator;

    protected virtual void OnEnable() {
        if (combatInput == null) return;
        combatInput.OnPress += InvokePressBind;
        combatInput.OnRealesed += InvokeReleasedBind;
    }

    protected virtual void OnDisable() {
        if (combatInput == null) return;
        combatInput.OnPress += InvokePressBind;
        combatInput.OnRealesed += InvokeReleasedBind;
    }
    
    
    protected virtual void InvokePressBind() {
        OnPressedBind?.Invoke(this);
    }
    
    protected virtual void InvokeReleasedBind() {
        OnReleasedBind?.Invoke(this);
    }
    
    public void AnimTriggerHandler() {
        if (!IsAnimating()) return;
        OnTriggerEvent?.Invoke(this);
    }

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
    
    public IEnumerator StopBeforeAnimReachTime(float animationPercent) {
        yield return new WaitUntil(() => {
            var animInfo = animator.GetCurrentAnimatorStateInfo(0);
            var stop = animInfo.length * Mathf.Clamp(animationPercent / 100,0f,0.99f);
            var currentAnimFrame = animInfo.normalizedTime * animInfo.length;
            return currentAnimFrame > stop;
        });
    }
    
    protected IEnumerator WaitForEndAnimation(string nameAnimation) {
        yield return new WaitUntil(() => !animator.GetCurrentAnimatorStateInfo(0).IsName(nameAnimation));
    }
    
    public abstract void MakingAttack();
    public abstract float GetDamage();
    public abstract bool IsAnimating();
    public abstract bool BlockingMovement();
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
