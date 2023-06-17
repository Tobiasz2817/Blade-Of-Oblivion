using System;
using System.Collections;
using UnityEngine;

public abstract class CollisionMaker : MonoBehaviour
{
    [SerializeField] protected LayerMask targetLayerMask;
    [SerializeField] protected bool displayGizmos;

    [SerializeField] protected Color collisionColor;
    [SerializeField] protected Color noneCollisionColor;
    
    public Action<CollisionHit> OnTargetHit;
    protected CollisionHit potentiallyHit = new CollisionHit();

    public void SendCollisionCoroutine(float targetTime, bool turnOffWhenAllCollisionsTrigger, bool turnOffOnFirstCollision = true) {
        StopAllCoroutines();
        StartCoroutine(SendCollisionWhileTimeEnd(targetTime, turnOffWhenAllCollisionsTrigger, turnOffOnFirstCollision));
    }
    
    public void StopMakingCollision(float timeToStopExecute) {
        StartCoroutine(DisableCollisionsCoroutine(timeToStopExecute));
    }

    private IEnumerator DisableCollisionsCoroutine(float timeToStopExecute) {
        yield return new WaitForSeconds(timeToStopExecute);
        StopAllCoroutines();
    }

    public abstract void SendCollision(bool turnOffOnFirstCollision = true);
    public abstract CollisionHit[] GetHits(bool turnOffOnFirstCollision = true);
    protected abstract IEnumerator SendCollisionWhileTimeEnd(float targetTime,bool turnOffWhenAllCollisionsTrigger, bool turnOffOnFirstCollision = true);
    protected virtual void OnDrawGizmos() { }
}
