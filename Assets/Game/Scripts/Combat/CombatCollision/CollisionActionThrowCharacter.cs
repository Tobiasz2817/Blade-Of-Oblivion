using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class CollisionActionThrowCharacter : CollisionActionHandler
{
    [SerializeField] private float throwPower = 15f;
    [SerializeField] private float throwDistance = 2f;
    [SerializeField] private Vector3Direction direction;
    [SerializeField] private ForceMode forceMode;
    [SerializeField] private bool relativeOnTargetTransform;

    private Coroutine throwInDirection;
    
    public override void MakeAction(CollisionHit collisionHit) {
        var direction = relativeOnTargetTransform
            ? Vector3DirectionCaster.CastDirectionOnTransform(this.direction,transform)
            : Vector3DirectionCaster.CastOnVector3(this.direction);
        if(throwInDirection != null) StopCoroutine(throwInDirection);

        
        if (collisionHit.collider) {
            if (collisionHit.collider.TryGetComponent(out Rigidbody rigidbody)) {
                rigidbody.AddForce(direction * throwPower * Time.deltaTime,forceMode);
            }
            else if (collisionHit.collider.TryGetComponent(out CharacterController characterController)) {
                //characterController.transform.position + direction * throwDistance)
                throwInDirection = StartCoroutine(ThrowInDirection(characterController,direction));
                //StartCoroutine(StopCoroutines(1f));
            }
        }
    }

    private IEnumerator ThrowInDirection(CharacterController characterController,Vector3 direction) {
        float elapsedTime = 0f;
        var initialPosition = characterController.transform.position;
        while (elapsedTime < 1f) 
        {
            float normalizedTime = elapsedTime / (throwDistance / throwPower);
            float currentDistance = normalizedTime * throwDistance;
            
            Vector3 targetPosition = initialPosition + direction.normalized * currentDistance;
            
            characterController.Move((targetPosition - transform.position) * Time.deltaTime);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

    }

    private IEnumerator StopCoroutines(float time) {
        yield return new WaitForSeconds(time);
        StopAllCoroutines();
    }
}

/*var t = 0f;
Debug.Log("Direction: " + direction);
while (t < 1f) {
var nextPos = Vector3.Lerp(characterController.transform.position, direction, t * Time.deltaTime);
Debug.Log("NextPos: " + nextPos);
//Debug.Log(characterController.transform.position);
characterController.Move(nextPos * Time.deltaTime * throwPower);
t += Time.deltaTime;
yield return null;
}*/