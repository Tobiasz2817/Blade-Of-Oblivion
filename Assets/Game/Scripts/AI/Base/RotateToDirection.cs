using Panda;
using UnityEngine;
using UnityEngine.AI;

public class RotateToDirection : MonoBehaviour
{
    [SerializeField] private NavMeshAgent navMeshAgent;
    [SerializeField] private float rotateSpeed = 15f;
    
    private void OnEnable() {
        navMeshAgent.updateRotation = false;
    }
    private void OnDisable() {
        navMeshAgent.updateRotation = true;
    }

    [Task]
    public void RotateToPlayer() {
        if (!PlayerSingleton.Instance) {
            Task.current.Fail();
            return;
        }
        
        RotateTo(PlayerSingleton.Instance.GetPosition().position);
        Task.current.Succeed();
    }

    [Task]
    public void RotateToNavMeshDestination() {
        RotateTo(navMeshAgent.destination);
        Task.current.Succeed();
    }

    private void RotateTo(Vector3 direction) {
        var lookPos = (direction - transform.position).normalized;
        lookPos.y = 0;
        var rotation = Quaternion.LookRotation(lookPos);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * rotateSpeed);
    }
}
