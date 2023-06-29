using UnityEngine;
using UnityEngine.UI;

public class HealthInterface : MonoBehaviour
{
    [SerializeField] private Health health;
    [SerializeField] private Image image;
    
    private void OnEnable() {
        health.OnTakeDamage.AddListener(UpdateInterface);
        health.OnHealthUpdate.AddListener(UpdateInterface);
    }

    private void UpdateInterface(float newHealth) {
        image.fillAmount = health.GetHealth() / health.GetMaxHealth();
    }
}
