using UnityEngine;

public class CollisionActionDealDamage : CollisionActionHandler
{
    [SerializeField] private DamageBasedHandler damageBasedHandler;
    
    public override void MakeAction(CollisionHit collisionHit) {
        TakeDamage(collisionHit);
    }
    
    private void TakeDamage(CollisionHit collisionHit) {
        Debug.Log("Injection damage");
        if (collisionHit.collider.TryGetComponent(out Health health_)) {
            health_.TakeDamage(damageBasedHandler.GetDamage());
        }
    }
}
