using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CombatAttack2 : Attack
{
    private Coroutine invokingCombo;
    private CountMousePress countMousePress;
    
    [Serializable]
    public class ComboDependencies
    {
        [field:SerializeField] public int priorty { private set; get; }
        [field:SerializeField] public SingleAttack singleAttack { private set; get; } 
        [field:SerializeField] public float incrementSpeedAnim { private set; get; } = 0.2f;
        [field:SerializeField] public string speedAnimationFloat { private set; get; }
    }

    [SerializeField] private bool basedOnMousePressing = false;
    [SerializeField] protected List<ComboDependencies> comboDependenciesList = new List<ComboDependencies>();
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
        return currentDependencies != null ? currentDependencies.singleAttack.GetDamage() : 0f;
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
            SpeedAnimation(attack.incrementSpeedAnim,attack.speedAnimationFloat);
            yield return attack.singleAttack.MakingCombo();
            BackSpeed(attack.speedAnimationFloat);
            OnExecuteAnimAttack?.Invoke(this);
        }
        
        currentDependencies = null;
        isAnimating = false;
    }
    
    /*
     *     private IEnumerator MakingCombo() {
        OnStartAnimAttack?.Invoke(this);
        foreach (var attack in comboDependenciesList) {
            attack.singleAttack.OnStartAnimAttack?.Invoke(attack.singleAttack);
            currentDependencies = attack;
            animator.Play(attack.singleAttack.GetAnimationClip().name);
            SpeedAnimation(attack.incrementSpeedAnim,attack.speedAnimationFloat);
            yield return new WaitForSeconds(0.02f);
            //if (attack.animationClip == comboDependenciesList.Last().animationClip) yield return WaitForEndAnimation(attack.animationClip.name);
            yield return StopBeforeSomeTime(attack.singleAttack.GetBreakTime());
            BackSpeed(attack.speedAnimationFloat);
            OnExecuteAnimAttack?.Invoke(this);
            attack.singleAttack.OnExecuteAnimAttack?.Invoke(attack.singleAttack);
            if(!basedOnMousePressing) continue;
            if (!countMousePress.ButtonWasPressedLastTime(0.3f)) break;
        }
        
        currentDependencies = null;
        countMousePress.ResetTime();
        isAnimating = false;
    }
     */
    
    private void IncrementPress() {
        if (!basedOnMousePressing) return;
        countMousePress.IncrementPressing();
    }
    
    protected override void InvokePressBind() {
        base.InvokePressBind();
        IncrementPress();
    }

    public bool IsInvokeLastAnimation() {
        if (currentDependencies == null) return false;

        return currentDependencies == comboDependenciesList[^1];
    }
}
