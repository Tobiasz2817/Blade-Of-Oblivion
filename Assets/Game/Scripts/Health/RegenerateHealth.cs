using System.Collections;
using UnityEngine;

public class RegenerateHealth : MonoBehaviour
{
    [SerializeField] protected Health health;
    [SerializeField] protected float regenerateSpeed;

    protected Coroutine regeneratingCoroutine;
    
    protected void RegeneratingHealth() {
        if (regeneratingCoroutine != null) return;

        regeneratingCoroutine = StartCoroutine(Regenerating());
    }

    private IEnumerator Regenerating() {
        while (health.GetHealth() < health.GetMaxHealth()) {
            health.ExpandCurrentHealth(regenerateSpeed * Time.deltaTime);
            yield return null;
        }

        regeneratingCoroutine = null;
    }
}
