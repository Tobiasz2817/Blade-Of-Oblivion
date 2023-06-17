using UnityEngine;

public class DamageReceiver : MonoBehaviour
{
    [SerializeField] private Weapon weapon;
    [SerializeField] private CombatManager combatManager;
    [SerializeField] private CollisionMaker collisionMaker;

    private void OnEnable() {
        collisionMaker.OnTargetHit += TakeDamage;
    }

    private void TakeDamage(CollisionHit obj) {
        if (obj.collider.TryGetComponent(out Health health_)) {
            health_.TakeDamage(GetDamage());
        }
    }
    
    public float GetDamage() {
        return weapon.GetWeaponDamage() + combatManager.GetAnimationDamage();
    }
}
