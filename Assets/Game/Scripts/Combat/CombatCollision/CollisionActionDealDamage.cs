using UnityEngine;

public class CollisionActionDealDamage : CollisionActionHandler
{
    [SerializeField] private DamageBasedHandler damageBasedHandler;
    [SerializeField] private JumpInDirection rollingReference;
    
    public override void MakeAction(CollisionHit collisionHit) {
        if (rollingReference.IsJumping) return;
        TakeDamage(collisionHit);
    }
    
    private void TakeDamage(CollisionHit collisionHit) {
        Debug.Log("Injection damage");
        if (collisionHit.collider.TryGetComponent(out Health health_)) {
            health_.TakeDamage(damageBasedHandler.GetDamage());
        }
    }
}
