using System;
using System.Collections.Generic;
using UnityEngine;

public class CombatManager : MonoBehaviour
{
    [SerializeField] private List<Attack> attacks = new List<Attack>();
    [SerializeField] private bool isGettingReferencesFromChild = true;
    
    private bool processingHit = false;

    private Attack currentAttack;
    private void Awake() {
        if (isGettingReferencesFromChild)
            GetAttackReferencesFromChild();
    }

    private void OnEnable() {
        SubscribeEventsFromList();
    }

    public void SubscribeEventsFromList() {
        foreach (var attacking in attacks) {
            attacking.OnPressedBind += DoSomething;
        }
    }

    private void OnDisable() {
        foreach (var attacking in attacks) {
            attacking.OnPressedBind -= DoSomething;
        }
    }

    public void GetAttackReferencesFromChild() {
        attacks.Clear();
        foreach (var attack in GetComponentsInChildren<Attack>()) {
            attacks.Add(attack);
        }
    }

    private void DoSomething(Attack obj) {
        if (IsSomeAttackInvoke()) {
            return;
        }
       
        obj.MakingAttack();
        currentAttack = obj;
    }

    public bool IsSomeAttackInvoke() {
        foreach (var attack in attacks) 
            if (attack.IsAnimating()) 
                return true;

        return false;
    }
    
    public bool IsCurrentAnimationBlockMovement() {
        foreach (var attack in attacks) 
            if (attack.IsAnimating()) 
                if(attack.BlockingMovement())
                    return true;

        return false;
    }
    

    public float GetAnimationDamage() {
        return currentAttack != null ? currentAttack.GetDamage() : 0f;
    }
}
