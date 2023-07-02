using System;
using System.Collections.Generic;
using UnityEngine;

public class CombatCollisionHandler : MonoBehaviour
{
    [field: SerializeField] public float TargetTime { private set; get; } = 2f;
    [field: SerializeField] public bool TurnOffWhenAllMarkersTrigger { private set; get; } = false;
    [field: SerializeField] public bool TurnOffOnFirstCollision { private set; get; } = true;
    [field: SerializeField] public bool TurnOffCollisionAfterAnimationEnd { private set; get; } = true;
    [field: SerializeField] public List<Attack> Attack { private set; get; } = new List<Attack>();
    [field: SerializeField] public CollisionMaker CollisionMaker { private set; get; }

    public CollisionType collisionType;
    public Action<CollisionHit> OnCollision;

    private Attack currentAnimationAttack;
    private void OnEnable() {
        Attack.ForEach((attack) => {
            attack.OnTriggerEvent += EnableCollision;
            attack.OnExecuteAnimAttack += DisableCollision;
        });

        CollisionMaker.OnTargetHit += SubscribeCollision;
    }
    
    private void OnDisable() {
        Attack.ForEach((attack) => {
            attack.OnTriggerEvent -= EnableCollision;
            attack.OnExecuteAnimAttack -= DisableCollision;
        });
        CollisionMaker.OnTargetHit -= SubscribeCollision;
    }

    private void SubscribeCollision(CollisionHit collisionHit) {
        if (Attack.Contains(currentAnimationAttack)) 
            OnCollision?.Invoke(collisionHit);

        currentAnimationAttack = null;
    }
    
    private void EnableCollision(Attack attackRef) {
        currentAnimationAttack = attackRef;

        switch (collisionType) {
            case CollisionType.SingleCollision: {
                CollisionMaker.SendCollision(TurnOffOnFirstCollision);
            }
                break;
            case CollisionType.EndlessCollision: {
                CollisionMaker.SendCollisionCoroutine(TargetTime,TurnOffWhenAllMarkersTrigger,TurnOffOnFirstCollision);
            }
                break;
        }
    }
    
    private void DisableCollision(Attack attackRef) {
        currentAnimationAttack = null;
        if (!TurnOffCollisionAfterAnimationEnd) return;
        
        CollisionMaker.StopMakingCollision(0f);
    }
    
    public enum CollisionType
    {
        EndlessCollision,
        SingleCollision
    }
}
