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


public class Cooldown : MonoBehaviour
{
    private bool isCooldown = false;
    
    [Task]
    public void CooldownTime(float time) {
        if (!isCooldown) {
            isCooldown = true;
            StartCoroutine(CooldownCounter(time));
            Task.current.Succeed();
            return;
        }
        
        
        Task.current.Fail();
    }
    private IEnumerator CooldownCounter(float time)
    {
        yield return new WaitForSeconds(time);
        isCooldown = false;
    }
}