using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ComboAttack : Attack
{
    private Coroutine invokingCombo;
    private CountMousePress countMousePress;
    
    [Serializable]
    public class ComboDependencies
    {
        [field:SerializeField] public int priorty { private set; get; }
        [field:SerializeField] public float damage { private set; get; }
        [field:SerializeField] public float breakTime { private set; get; } = 0.2f;
        [field:SerializeField] public float incrementSpeedAnim { private set; get; } = 0.2f;
        [field:SerializeField] public float startMotionSpeed { private set; get; } = 1f;
        [field:SerializeField] public string speedAnimationFloat { private set; get; }
        [field:SerializeField] public AnimationClip animationClip { private set; get; }
    }

    [SerializeField]
    protected List<ComboDependencies> comboDependenciesList = new List<ComboDependencies>();

    private ComboDependencies currentDependencies;
    
    private void Start() {
        countMousePress = new CountMousePress();
        comboDependenciesList = comboDependenciesList.OrderBy((valeus) => valeus.priorty).ToList();
    }

    public override void MakingAttack() {
        if (isAnimating) return;
        StartCoroutine(MakingCombo());
        isAnimating = true;
    }

    public override float GetDamage() {
        return currentDependencies != null ? currentDependencies.damage : 0f;
    }

    public override bool IsAnimating() {
        return isAnimating;
    }

    public override bool BlockingMovement() {
        return true;
    }

    private IEnumerator MakingCombo() {
        OnStartAnimAttack?.Invoke(this);
        foreach (var attack in comboDependenciesList) {
            currentDependencies = attack;
            animator.Play(attack.animationClip.name);
            SpeedAnimation(attack.incrementSpeedAnim,attack.speedAnimationFloat);
            yield return new WaitForSeconds(0.02f);
            //if (attack.animationClip == comboDependenciesList.Last().animationClip) yield return WaitForEndAnimation(attack.animationClip.name);
            yield return StopBeforeSomeTime(attack.breakTime);
            BackSpeed(attack.startMotionSpeed,attack.speedAnimationFloat);
            OnExecuteAnimAttack?.Invoke(this);
            if (!countMousePress.ButtonWasPressedLastTime(0.3f)) break;
        }
        
        currentDependencies = null;
        countMousePress.ResetTime();
        isAnimating = false;
    }
    
    private void IncrementPress() {
        countMousePress.IncrementPressing();
    }
    
    protected override void InvokePressBind() {
        base.InvokePressBind();
        IncrementPress();
    }
}