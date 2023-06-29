using System.Collections.Generic;
using UnityEngine;

public class DamageInjection : MonoBehaviour
{
    [SerializeField] private List<CollisionActionHandler> collisionActionHandlers = new List<CollisionActionHandler>();
    [SerializeField] private List<CombatCollisionHandler> collisionHandlers = new List<CombatCollisionHandler>();

    private void OnEnable() {
        collisionHandlers.ForEach((handler) => handler.OnCollision += MakeAction);
    }
    
    private void OnDisable() {
        collisionHandlers.ForEach((handler) => handler.OnCollision -= MakeAction);
    }

    private void MakeAction(CollisionHit collisionHit) {
        collisionActionHandlers.ForEach((action) => action.MakeAction(collisionHit));
    }
}
