using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class LoopAttack : Attack
{
    [Serializable]
    public class LoopDependencies
    {
        [field:SerializeField] public float unlockTime { private set; get; } = 10f;
        [field:SerializeField] public float loopTime { private set; get; } = 4f;
        [field:SerializeField] public float damage { private set; get; } = 20f;
        [field:SerializeField] public float stopValue { private set; get; } = 0f;
        
        [field:SerializeField] public float startAnimSpeed { private set; get; } = 1f;
        
        [field:SerializeField] public float incrementSpeedAnim { private set; get; } = 0.2f;
        
        [field:SerializeField] public string speedAnimationFloat { private set; get; }
        [field:SerializeField] public string returnParameter { private set; get; }
        [field:SerializeField] public AnimationClip animationClip { private set; get; }
        [field:SerializeField] public Image display { private set; get; }
    }

    [SerializeField] private LoopDependencies loopDependencies;
    private bool unlocking = false;
    public override void MakingAttack() {
        if (isAnimating || unlocking) return;
        StartCoroutine(MakingCombo());
        isAnimating = true;
    }

    public override float GetDamage() {
        return loopDependencies.damage;
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
        animator.Play(loopDependencies.animationClip.name);
        OnStartAnimAttack?.Invoke(this);
        yield return new WaitForSeconds(0.02f);
        SpeedAnimation(loopDependencies.incrementSpeedAnim,loopDependencies.speedAnimationFloat);
        while (currentTime > loopDependencies.stopValue) {
            loopDependencies.display.fillAmount = (currentTime / loopDependencies.loopTime);
            currentTime -= Time.deltaTime;
            yield return null;
        }
        BackSpeed(loopDependencies.startAnimSpeed,loopDependencies.speedAnimationFloat);
        animator.SetBool(loopDependencies.returnParameter,true);
        StartCoroutine(UnlockAfterTime());
        isAnimating = false;
    }

    protected override void InvokeReleasedBind() {
        unlocking = true;
        StopAllCoroutines();
        BackSpeed(loopDependencies.startAnimSpeed,loopDependencies.speedAnimationFloat);
        animator.SetTrigger(loopDependencies.returnParameter);
        StartCoroutine(UnlockAfterTime());
        isAnimating = false;
    }

    private IEnumerator UnlockAfterTime() {
        if (isAnimating) OnExecuteAnimAttack?.Invoke(this);
        
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
