using Panda;
using UnityEngine;

public class CombatEnemyInput : CombatInput
{
    [SerializeField] public string name;
    [SerializeField] public bool isOnCooldown;
    [SerializeField] public float cooldownTime;
    public float lastUsed = -1;

    [Task]
    public void Attack() {
        OnPress?.Invoke();
    }
}
