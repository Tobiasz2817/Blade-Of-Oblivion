using UnityEngine;

public  class DamageValueHandler : DamageBasedHandler
{
    [SerializeField] private Weapon weapon;
    [SerializeField] private CombatManager combatManager;
    
    public override float GetDamage() {
        return combatManager.GetAnimationDamage() + weapon.GetWeaponDamage();
    }
}
