
using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour
{
    public UnityEvent<float> OnHealthUpdate;
    public UnityEvent<float> OnTakeDamage;
    public UnityEvent<float> OnMaxHealthUpdate;
    
    [SerializeField] private float maxHealth = 100;
    [SerializeField] private float currentHealth = 100;
    
    public float GetHealth() => currentHealth;
    public float GetMaxHealth() => maxHealth;

    public void ExpandMaxHealth(float newMaxHealth) {
        this.maxHealth = newMaxHealth;
        OnMaxHealthUpdate?.Invoke(newMaxHealth);
    }

    public void ExpandCurrentHealth(float addHealth) {
        currentHealth = Mathf.Clamp(addHealth + currentHealth,0, maxHealth);
        OnHealthUpdate?.Invoke(currentHealth);
    }

    public void TakeDamage(float damage) {
        currentHealth = Mathf.Clamp(currentHealth - damage, 0, maxHealth);
        OnTakeDamage?.Invoke(currentHealth);
    }
}
