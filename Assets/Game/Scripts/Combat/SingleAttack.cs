using System;
using System.Collections;
using UnityEngine;

public class SingleAttack : Attack
{
    [Serializable]
    public class SingleAttackDependencies
    {
        [field:SerializeField] public float damage { private set; get; }
        [field:SerializeField] public Motion motion { private set; get; }
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
        animator.Play(singleAttack.motion.name);
        yield return new WaitForSeconds(0.02f);
        yield return WaitForEndAnimation(singleAttack.motion.name);
        isAnimating = false;
    }
}
