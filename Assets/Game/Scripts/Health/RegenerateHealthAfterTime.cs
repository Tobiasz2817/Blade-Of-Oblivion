using System.Collections;
using UnityEngine;

public class RegenerateHealthAfterTime : RegenerateHealth
{
    [SerializeField] private float regeneratingTime = 5f;

    private void OnEnable() {
        health.OnTakeDamage.AddListener(OnGetDamage);
    }
    
    private void OnDisable() {
        health.OnTakeDamage.RemoveListener(OnGetDamage);
    }
    
    private void OnGetDamage(float arg0) {
        if (regeneratingCoroutine != null) {
            StopCoroutine(regeneratingCoroutine);
            regeneratingCoroutine = null;
        }
        StopAllCoroutines();
        StartCoroutine(WaitForRegenerating());
    }

    private IEnumerator WaitForRegenerating() {
        yield return new WaitForSeconds(regeneratingTime);
        RegeneratingHealth();
    }
}
