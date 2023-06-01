using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class LoopAttack : Attack
{
    [Serializable]
    public class LoopDependencies
    {
        public string returnParameter;
        public float loopTime = 4f;
        public float stopValue = 0f;
        public float unlockTime = 10f;
        public Motion motion;
        public Image display;
    }

    [SerializeField] private LoopDependencies loopDependencies;
    private bool unlocking = false;
    public override void MakingAttack() {
        if (isAnimating || unlocking) return;
        StartCoroutine(MakingCombo());
        isAnimating = true;
    }

    public override bool IsAnimating() {
        return isAnimating;
    }

    public override bool BlockingMovement() {
        return false;
    }

    private IEnumerator MakingCombo() {
        var currentTime = loopDependencies.loopTime;
        animator.SetBool(loopDependencies.returnParameter,false);
        animator.Play(loopDependencies.motion.name);
        yield return new WaitForSeconds(0.02f);
        while (currentTime > loopDependencies.stopValue) {
            loopDependencies.display.fillAmount = (currentTime / loopDependencies.loopTime);
            currentTime -= Time.deltaTime;
            yield return null;
        }
        animator.SetBool(loopDependencies.returnParameter,true);
        
        StartCoroutine(UnlockAfterTime());
        isAnimating = false;
    }

    protected override void InvokeAttackBind(InputAction.CallbackContext callbackContext) {
        base.InvokeAttackBind(callbackContext);
        if(callbackContext.canceled) {
            unlocking = true;
            isAnimating = false;
            StopAllCoroutines();
            animator.SetTrigger(loopDependencies.returnParameter);
            StartCoroutine(UnlockAfterTime());
        }
    }

    private IEnumerator UnlockAfterTime() {
        var tmp = loopDependencies.display.fillAmount * loopDependencies.loopTime;
        var startValue = tmp;
        var restUnlockTime = Mathf.Abs(loopDependencies.unlockTime - (loopDependencies.unlockTime * (tmp / loopDependencies.loopTime)));
        while (tmp < loopDependencies.loopTime) {
            tmp += Time.deltaTime * (loopDependencies.loopTime - startValue) / restUnlockTime;
            var clampedValue = Mathf.Clamp(tmp, loopDependencies.stopValue, loopDependencies.loopTime);
            loopDependencies.display.fillAmount = clampedValue / loopDependencies.loopTime;
            yield return null;
        }
        
        unlocking = false;
        isAnimating = false;
    }
}
