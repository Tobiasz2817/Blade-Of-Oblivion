using System;
using System.Collections.Generic;
using Panda;
using UnityEngine;
using UnityEngine.Events;

public class InvokeAnims : MonoBehaviour
{
    [Serializable]
    public class Dependencies
    {
        public string name;
        public Attack anim;
    }

    [SerializeField] private List<Dependencies> anims = new List<Dependencies>();

    private Dictionary<string, Attack> attacks = new Dictionary<string, Attack>();

    public UnityEvent OnBuff;

    public float deathTime = 4f;
    public Animator animator;

    private void Start() {
        foreach (var anim in anims) {
            anim.anim.OnStartAnimAttack += StartAnim;
            anim.anim.OnExecuteAnimAttack += EndAnim;
            attacks.Add(anim.name,anim.anim);
        }
    }

    private bool isAnimEnd = false;
    
    private void StartAnim(Attack obj) {
        isAnimEnd = false;
    }
    
    private void EndAnim(Attack obj) {
        foreach (var attack in attacks) {
            if (obj == attack.Value) {
                if (attack.Key == "Buff")
                    OnBuff?.Invoke();
            }
        }

        isAnimEnd = true;
    }

    [Task]
    public void InvokeAnim(string name) {
        attacks[name].OnPressedBind?.Invoke(attacks[name]);
        Task.current.Succeed();
    }
    
    public void InvokeAnimA(string name) { 
        animator.Play(name);
    }
}
