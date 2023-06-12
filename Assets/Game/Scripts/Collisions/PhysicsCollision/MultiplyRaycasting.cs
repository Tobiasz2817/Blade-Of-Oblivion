using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiplyRaycasting : CollisionMaker
{
    [Serializable]
    public class RayDependencies
    {
        public float rayDistance;
        public Transform rayPoint;
        [Tooltip("Direction based on local axis direction")]
        public Vector3Direction direction;
    }
    
    [SerializeField] private List<RayDependencies> rayDependencies;


    public override void SendCollision(bool turnOffOnFirstCollision = true) {
        foreach (var rayDependency in rayDependencies) {
            var ray = new Ray(rayDependency.rayPoint.position, rayDependency.rayPoint.TransformDirection(Vector3DirectionCaster.CastOnVector3(rayDependency.direction)));
            if (!Physics.Raycast(ray, out RaycastHit info, rayDependency.rayDistance, targetLayerMask)) continue;
            OnTargetHit?.Invoke(potentiallyHit.SetCollisionHit(info.collider, info.point));
            if (turnOffOnFirstCollision) return;
        }
    }

    protected override IEnumerator SendCollisionWhileTimeEnd(float targetTime,bool turnOffWhenAllCollisionsTrigger,bool turnOffOnFirstCollision = true) {
        StopMakingCollision(targetTime);
        var wasTrigger = false;
        while (true) {
            foreach (var rayDependency in rayDependencies) {
                var ray = new Ray(rayDependency.rayPoint.position, rayDependency.rayPoint.TransformDirection(Vector3DirectionCaster.CastOnVector3(rayDependency.direction)));
                if (!Physics.Raycast(ray, out RaycastHit info, rayDependency.rayDistance, targetLayerMask)) continue;
                OnTargetHit?.Invoke(potentiallyHit.SetCollisionHit(info.collider, info.point));
                wasTrigger = true;
                if (turnOffOnFirstCollision) yield break;
            }

            if (wasTrigger && turnOffWhenAllCollisionsTrigger) yield break; 
            
            yield return null;
        }
    }

    public override CollisionHit[] GetHits(bool turnOffOnFirstCollision = true) {
        CollisionHit[] hits = new CollisionHit[rayDependencies.Count];

        for (int i = 0; i < rayDependencies.Count; i++) {
            var ray = new Ray(rayDependencies[i].rayPoint.position, rayDependencies[i].rayPoint.TransformDirection(Vector3DirectionCaster.CastOnVector3(rayDependencies[i].direction)));
            if (!Physics.Raycast(ray, out RaycastHit info, rayDependencies[i].rayDistance, targetLayerMask)) continue;
            hits[i] = potentiallyHit.SetCollisionHit(info.collider, info.point);
            if (turnOffOnFirstCollision) return hits;
        }

        return hits;
    }
    
    protected override void OnDrawGizmos() {
        if (!displayGizmos) return;

        foreach (var rayDependency in rayDependencies) {
            if(!rayDependency.rayPoint) continue;
            var ray = new Ray(rayDependency.rayPoint.position, rayDependency.rayPoint.TransformDirection(Vector3DirectionCaster.CastOnVector3(rayDependency.direction)));
            var isCollide = Physics.Raycast(ray, out RaycastHit info, rayDependency.rayDistance, targetLayerMask);
            var direction = isCollide ? info.point - ray.origin : ray.direction * rayDependency.rayDistance;
            Gizmos.color = isCollide ? collisionColor : noneCollisionColor;
            Gizmos.DrawRay(ray.origin,direction);
        }
    }
}

public enum Vector3Direction
{
    Forward,
    Back,
    Up,
    Down,
    Left,
    Right,
    One,
    Zero
}