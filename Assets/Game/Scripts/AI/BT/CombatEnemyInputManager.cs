using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Panda;

public class CombatEnemyInputManager : MonoBehaviour
{
    [SerializeField] private Dictionary<string, CombatEnemyInput> input = new Dictionary<string, CombatEnemyInput>();
    SkillCooldown skillCooldown;
    CombatEnemymanager combatManager;
    Animator animator;
    private void Start()
    {
        combatManager = GetComponent<CombatEnemymanager>();
        skillCooldown = GetComponent<SkillCooldown>();
        animator = GetComponentInChildren<Animator>();
        foreach (var item in GetComponentsInChildren<CombatEnemyInput>())
        {
            input.Add(item.name,item);
        }
    }
    [Task]
    public void Attack(string name)
    {           
        if (!combatManager.IsSomeAttackInvoke())
        {
            input[name].OnPress?.Invoke();
                Task.current.Succeed();
        }
    }
    [Task]
    public void SetCooldown(string name)
    {
        input[name].isOnCooldown = true;
        input[name].lastUsed = Time.time;
        Task.current.Succeed();
    }
    [Task]
    public bool IsOnCooldown(string name)
    {
        if (input[name].isOnCooldown)
        {
            return true;
        }    
        else
        {
            return false;
        }    
    }
}
