using System.Collections;
using System.Collections.Generic;
using Panda;
using UnityEngine;

public class CombatEnemyInputTMP : MonoBehaviour
{
    private Dictionary<string, CombatEnemyInput> input = new Dictionary<string, CombatEnemyInput>();

    private void Start() {
        foreach (var combat in GetComponents<CombatEnemyInput>()) {
            input.Add(combat.name,combat);
        }
    }

    [Task]
    public void Attack(string name) {
        input[name].OnPress?.Invoke();
    }
}

