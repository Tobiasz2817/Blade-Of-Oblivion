using System.Collections.Generic;
using Panda;
using UnityEngine;

public class AttackStatesController : MonoBehaviour
{
    [SerializeField] private List<AttackState> attackStates = new List<AttackState>();

    private Dictionary<string, AttackState> attacks = new Dictionary<string, AttackState>();

    private void Start() {
        foreach (var attackState in attackStates) {
            attacks.Add(attackState.nameState,attackState);
        }   
    }

    [Task]
    public bool StateIsFinished(string nameState) {
        return attacks[nameState].IsFinished;
    }

    [Task]
    public void ResetStates() {
        foreach (var attack in attacks) {
            attack.Value.IsFinished = false;
        }
        Task.current.Succeed();
    }
    
    [Task]
    public void InvokeAnimations(string nameState) {
        Task.current.Fail();
        attacks[nameState].InvokeAttack();
    }

    [Task]
    public bool IsAnimating(string nameState) {
        return attacks[nameState].IsAnimating;
    }

    [Task]
    public void SucceedExitState() {
        Task.current.Succeed();
    }
}
