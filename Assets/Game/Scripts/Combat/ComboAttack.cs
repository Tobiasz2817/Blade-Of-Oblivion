using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

public class ComboAttack : Attack
{
    private Coroutine invokingCombo;
    private CountMousePress countMousePress;
    
    [Serializable]
    public class ComboDependencies
    {
        [field:SerializeField] public int priorty { private set; get; }
        [field:SerializeField] public float damage { private set; get; }
        [field:SerializeField] public float breakTimeCombo { private set; get; } = 0.2f;
        [field:SerializeField] public float incrementSpeedAnim { private set; get; } = 0.2f;
        [field:SerializeField] public float startMotionSpeed { private set; get; } = 1f;
        [field:SerializeField] public string speedAnimationFloat { private set; get; }
        [field:SerializeField] public Motion motion { private set; get; }
    }

    [SerializeField]
    protected List<ComboDependencies> comboDependenciesList = new List<ComboDependencies>();

    private void Start() {
        countMousePress = new CountMousePress();
        comboDependenciesList = comboDependenciesList.OrderBy((valeus) => valeus.priorty).ToList();
    }

    public override void MakingAttack() {
        if (isAnimating) return;
        StartCoroutine(MakingCombo());
        isAnimating = true;
    }

    public override bool IsAnimating() {
        return isAnimating;
    }

    private IEnumerator MakingCombo() {
        foreach (var attack in comboDependenciesList) {
            animator.Play(attack.motion.name);
            SpeedAnimation(attack.incrementSpeedAnim,attack.speedAnimationFloat);
            yield return new WaitForSeconds(0.02f);
            if (attack.motion == comboDependenciesList.Last().motion) yield return WaitForEndAnimation(attack.motion.name);
            else yield return StopBeforeSomeTime(attack.breakTimeCombo);
            BackSpeed(attack.startMotionSpeed,attack.speedAnimationFloat);
            if (!countMousePress.ButtonWasPressedLastTime(0.3f)) break;
        }

        countMousePress.ResetTime();
        isAnimating = false;
    }
    
    private void IncrementPress() {
        countMousePress.IncrementPressing();
    }

    protected override void InvokeAttackBind(InputAction.CallbackContext callbackContext) {
        base.InvokeAttackBind(callbackContext);
        IncrementPress();
    }
}