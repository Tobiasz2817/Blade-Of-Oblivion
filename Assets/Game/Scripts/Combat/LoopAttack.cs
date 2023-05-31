using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class LoopAttack : Attack
{
    [Serializable]
    public class LoopDependencies
    {
        public string returnParameter;
        public float loopTime = 4f;
        public float stopValue = 0f;
        public Motion motion;
    }

    [SerializeField] private LoopDependencies loopDependencies;

    public override void MakingAttack() {
        if (isAnimating) return;
        StartCoroutine(MakingCombo());
        isAnimating = true;
    }

    public override bool IsAnimating() {
        return isAnimating;
    }
    
    private IEnumerator MakingCombo() {
        var currentTime = loopDependencies.loopTime;
        animator.SetBool(loopDependencies.returnParameter,false);
        animator.Play(loopDependencies.motion.name);
        yield return new WaitForSeconds(0.02f);
        while (currentTime > loopDependencies.stopValue) {
            currentTime -= Time.deltaTime;

            yield return null;
        }
        animator.SetBool(loopDependencies.returnParameter,true);
        
        isAnimating = false;
    }

    protected override void InvokeAttackBind(InputAction.CallbackContext callbackContext) {
        base.InvokeAttackBind(callbackContext);
        if(callbackContext.canceled) {
            isAnimating = false;
            StopAllCoroutines();
            animator.SetTrigger(loopDependencies.returnParameter);
        }
    }
}
