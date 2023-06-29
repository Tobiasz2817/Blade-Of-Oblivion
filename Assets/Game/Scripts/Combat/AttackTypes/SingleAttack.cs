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
        isAnimating = true;
    }

    public override float GetDamage() {
        return singleAttack.damage;
    }

    public override bool IsAnimating() {
        return isAnimating;
    }

    public override bool BlockingMovement() {
        return true;
    }

    private IEnumerator MakingCombo() {
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
