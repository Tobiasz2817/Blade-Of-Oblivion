using System.Collections.Generic;
using UnityEngine;

public class CombatManager : MonoBehaviour
{
    [SerializeField] private List<Attack> attacks = new List<Attack>();

    private bool canAttack = true;
    private bool processingHit = false;

    private Attack currentAttack;
    private void Start() {
        foreach (var attacking in attacks) 
            attacking.OnPressedBind += DoSomething;
    }

    private void DoSomething(Attack obj) {
        if (!canAttack) {
            return;
        }

        if (IsSomeAttackInvoke()) {
            return;
        }
       
        obj.MakingAttack();
    }
    

    public void SetCanAttack(bool canAttack_) {
        canAttack = canAttack_;
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
}
