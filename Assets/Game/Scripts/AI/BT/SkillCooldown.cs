using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Panda;

public class SkillCooldown : MonoBehaviour
{
    [SerializeField] private Dictionary<string, CombatEnemyInput> input = new Dictionary<string, CombatEnemyInput>();

    private void Start()
    {
        foreach (var item in GetComponentsInChildren<CombatEnemyInput>())
        {
            input.Add(item.name, item);
        }
    }
    private void Update()
    {
        foreach(var item in input)
        {   
            if(item.Value.isOnCooldown)
            {
                ResetCooldown(item.Value);
            }    
        }
    }
    public void ResetCooldown(CombatEnemyInput item)
    {
        if (Time.time - item.lastUsed >= item.cooldownTime)
        {
            item.isOnCooldown = false;
        }

    }
}
