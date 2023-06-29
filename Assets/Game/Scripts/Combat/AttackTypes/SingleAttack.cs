using System;
using System.Collections;
using UnityEngine;

public class SingleAttack : Attack
{
    [Serializable]
    public class SingleAttackDependencies
    {
        [field:SerializeField] public float damage { private set; get; }
        [field:SerializeField] public float breakTime { private set; get; }
        [field:SerializeField] public AnimationClip animationClip { private set; get; }
    }

    [SerializeField] private SingleAttackDependencies singleAttack;
    
    public override void MakingAttack() {
        StartCoroutine(MakingCombo());
    }

    public override float GetDamage() {
        return singleAttack.damage;
    }
    
    public AnimationClip GetAnimationClip() {
        return singleAttack.animationClip;
    }
    
    public float GetBreakTime() {
        return singleAttack.breakTime;
    }

    public override bool IsAnimating() {
        return isAnimating;
    }

    public override bool BlockingMovement() {
        return true;
    }

    public IEnumerator MakingCombo() {
        isAnimating = true;
        animator.Play(singleAttack.animationClip.name);
        OnStartAnimAttack?.Invoke(this);
        yield return new WaitForSeconds(0.02f);
        //yield return WaitForEndAnimation(singleAttack.animationClip.name);
        yield return StopBeforeSomeTime(singleAttack.breakTime);
        isAnimating = false;
        OnExecuteAnimAttack?.Invoke(this);
    }

    public float GetAnimationLength => singleAttack.animationClip.length;
}
