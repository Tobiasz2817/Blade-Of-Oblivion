using UnityEngine;
using UnityEngine.Events;

public class DamageReceiverHandler : MonoBehaviour
{   
    [SerializeField] private string nameCollisionTag;

    public UnityEvent<Collider> OnTrigger;
    public UnityEvent<Collision> OnCollision;

    private void OnTriggerEnter(Collider other) {
        if(other.CompareTag(nameCollisionTag))
            OnTrigger?.Invoke(other);
    }

    private void OnCollisionEnter(Collision other) {
        if(other.gameObject.CompareTag(nameCollisionTag))
            OnCollision?.Invoke(other);
    }
}
