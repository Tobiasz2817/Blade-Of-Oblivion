
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

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

    public override bool IsAnimating() {
        return isAnimating;
    }

    public override bool BlockingMovement() {
        return true;
    }

    protected override void InvokeAttackBind(InputAction.CallbackContext callbackContext) {
        base.InvokeAttackBind(callbackContext);
    }

    private IEnumerator MakingCombo() {
        animator.Play(singleAttack.motion.name);
        yield return new WaitForSeconds(0.02f);
        yield return WaitForEndAnimation(singleAttack.motion.name);
        isAnimating = false;
    }
}
