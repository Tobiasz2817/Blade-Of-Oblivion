using UnityEngine;

public class RegenerateWhenLostTrigger : RegenerateHealth   
{
    [SerializeField] private DamageReceiverHandler damageReceiverHandler;
    private void OnEnable() {
        damageReceiverHandler.OnTriggerOut.AddListener(OnExit);
    }
    private void OnDisable() {
        damageReceiverHandler.OnTriggerOut.RemoveListener(OnExit);
    }

    private void OnExit(Collider collider) {
        RegeneratingHealth();
    }
}
