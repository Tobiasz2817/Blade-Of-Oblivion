using UnityEngine;
using UnityEngine.Events;

public class DamageReceiverHandler : MonoBehaviour
{   
    [SerializeField] private string nameCollisionTag;

    public UnityEvent<Collider> OnTrigger;
    public UnityEvent<Collider> OnTriggerOut;

    private void OnTriggerEnter(Collider other) {
        if(other.CompareTag(nameCollisionTag))
            OnTrigger?.Invoke(other);
    }
    
    private void OnTriggerExit(Collider other) {
        if(other.CompareTag(nameCollisionTag))
            OnTriggerOut?.Invoke(other);
    }
}
