using UnityEngine;

public class ChangeStateObjectWhenTrigger : MonoBehaviour
{
    [SerializeField] private DamageReceiverHandler damageReceiverHandler;
    [SerializeField] private GameObject objectToChangeState;

    private void OnEnable() {
        damageReceiverHandler.OnTrigger.AddListener(OnEnter);
        damageReceiverHandler.OnTriggerOut.AddListener(OnExit);
    }
    private void OnDisable() {
        damageReceiverHandler.OnTrigger.RemoveListener(OnEnter);
        damageReceiverHandler.OnTriggerOut.RemoveListener(OnExit);
    }

    public void OnEnter(Collider collider) {
        objectToChangeState.SetActive(true);
    }
    
    public void OnExit(Collider collider) {
        objectToChangeState.SetActive(false);
    }
}
