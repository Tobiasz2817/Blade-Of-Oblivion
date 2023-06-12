using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiplySphereCollision : CollisionMaker
{
    [Serializable]
    public class SphereDependencies
    {
        public float sphereRadius;
        public Transform spherePoint;
    }
    
    [SerializeField] private List<SphereDependencies> sphereDependencies;
    
    private Collider[] colliders = new Collider[1];
    
    public override void SendCollision(bool turnOffOnFirstCollision = true) {
        foreach (var sphereDependency in sphereDependencies) {
            var collisionCount = Physics.OverlapSphereNonAlloc(sphereDependency.spherePoint.position, sphereDependency.sphereRadius,this.colliders ,targetLayerMask);
            if (collisionCount <= 0) return;
            OnTargetHit?.Invoke(potentiallyHit.SetCollisionHit(colliders[0]));
            if (turnOffOnFirstCollision) return;
        }
    }

    public override CollisionHit[] GetHits(bool turnOffOnFirstCollision = true) {
        CollisionHit[] hits = new CollisionHit[sphereDependencies.Count];

        for (int i = 0; i < sphereDependencies.Count; i++) {
            var collisionCount = Physics.OverlapSphereNonAlloc(sphereDependencies[i].spherePoint.position, sphereDependencies[i].sphereRadius,this.colliders ,targetLayerMask);
            if (collisionCount <= 0) continue;
            hits[i] = potentiallyHit.SetCollisionHit(colliders[0]);
            if (turnOffOnFirstCollision) return hits;
        }

        return hits;
    }

    protected override IEnumerator SendCollisionWhileTimeEnd(float targetTime, bool turnOffWhenAllCollisionsTrigger, bool turnOffOnFirstCollision = true) {
        StopMakingCollision(targetTime);
        var wasTrigger = false;
        while (true) {
            foreach (var sphereDependency in sphereDependencies) {
                var collisionCount = Physics.OverlapSphereNonAlloc(sphereDependency.spherePoint.position, sphereDependency.sphereRadius,this.colliders ,targetLayerMask);
                if(collisionCount <= 0) continue;
                OnTargetHit?.Invoke(potentiallyHit.SetCollisionHit(colliders[0]));
                wasTrigger = true;
                if (turnOffOnFirstCollision) yield break;
            }

            if (wasTrigger && turnOffWhenAllCollisionsTrigger) yield break; 
            
            yield return null;
        }
    }


    private Collider[] collidersDisplayer = new Collider[1];
    protected override void OnDrawGizmos() {
        if (!displayGizmos) return;

        foreach (var sphereDependency in sphereDependencies) {
            if(!sphereDependency.spherePoint) continue;
            var collidersCount = Physics.OverlapSphereNonAlloc(sphereDependency.spherePoint.position, sphereDependency.sphereRadius, collidersDisplayer ,targetLayerMask);
            Gizmos.color = collidersCount > 0 ? collisionColor : noneCollisionColor;
            Gizmos.DrawSphere(sphereDependency.spherePoint.position,sphereDependency.sphereRadius);
        }
    }
}

public struct CollisionHit
{
    public Collider collider { private set; get; }
    public Vector3 hitPoint { private set; get; }
    
    public CollisionHit(Collider collider) : this(collider,default) { }
    public CollisionHit(Vector3 hitPoint) : this(null,hitPoint) { }
    public CollisionHit(Collider collider, Vector3 hitPoint) {
        this.collider = collider;
        this.hitPoint = hitPoint;
    }

    public CollisionHit SetCollisionHit(Collider collider) {
        SetCollisionHit(collider, default);

        return this;
    }
    
    public CollisionHit SetCollisionHit(Vector3 hitPoint) {
        SetCollisionHit(null,hitPoint);
        
        return this;
    }
    
    public CollisionHit SetCollisionHit(Collider collider, Vector3 hitPoint) {
        this.collider = collider;
        this.hitPoint = hitPoint;
        
        return this;
    }
}