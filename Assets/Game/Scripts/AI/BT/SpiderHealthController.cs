using System.Collections;
using Panda;
using UnityEngine;
using UnityEngine.AI;

public class SpiderHealthController : MonoBehaviour
{
    public InvokeAnims anims;
    public BehaviourTree bt;
    public NavMeshAgent agent;
    public CombatManager combatManager;
    public CapsuleCollider collider;

    private bool isDead = false;
    private IEnumerator Wait() {
        yield return new WaitForSeconds(10f);
        Destroy(gameObject);
    }

    public void SpiderDead(float currentHealth) {
        if (isDead) return;
        if (currentHealth <= 0) {
            anims.InvokeAnimA("SpiderBoss_Death");
            bt.enabled = false;
            agent.enabled = false;
            collider.enabled = false;
            combatManager.enabled = false;
            StartCoroutine(Wait());

            isDead = true;
        }
    }

    [Task]
    public void ResetPath() {
        agent.ResetPath();
        agent.SetDestination(transform.position);
        Task.current.Succeed();
    }
}
