using System.Collections.Generic;
using UnityEngine;

public class AttackState : MonoBehaviour
{
    public string nameState;
    public bool IsFinished { set; get; } = false;
    public bool IsAnimating { set; get; } = false;

    private Attack currentAttack = null;
    private int currentAttackIndex = 0;
    public List<Attack> attacks = new List<Attack>();

    private void OnEnable() {
        foreach (var attack in attacks) {
            attack.OnStartAnimAttack += StartAnim;
            attack.OnExecuteAnimAttack += EndAnim;
        }
    }

    private void OnDisable() {
        foreach (var attack in attacks) {
            attack.OnStartAnimAttack -= StartAnim;
            attack.OnExecuteAnimAttack -= EndAnim;
        }
    }

    private void StartAnim(Attack obj) {
        currentAttack = obj;
        IsAnimating = true;
    }
    
    private void EndAnim(Attack obj) {
        if (currentAttack == attacks[^1]) {
            if (obj.GetType() == typeof(ComboAttack)) {
                if (((ComboAttack)attacks[currentAttackIndex]).IsInvokeLastAnimation()) {
                    IsFinished = true;
                    IsAnimating = false;
                }
            }
            else {
                IsFinished = true;
                IsAnimating = false;
            }
        }
        else
            currentAttackIndex++;
    }

    public void InvokeAttack() {
        attacks[currentAttackIndex].OnPressedBind?.Invoke(attacks[currentAttackIndex]);
    }
    
    
}
