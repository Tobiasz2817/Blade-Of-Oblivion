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
        
        RotateTo(PlayerSingleton.Instance.GetPosition().position, rotateSpeed);
        Task.current.Succeed();
    }
    
    [Task]
    public void RotateToPlayerWithSpeed(float speed) {
        if (!PlayerSingleton.Instance) {
            Task.current.Fail();
            return;
        }
        
        RotateTo(PlayerSingleton.Instance.GetPosition().position,speed);
        Task.current.Succeed();
    }

    [Task]
    public void RotateToNavMeshDestination() {
        RotateTo(navMeshAgent.destination,rotateSpeed);
        Task.current.Succeed();
    }

    private void RotateTo(Vector3 direction, float speed) {
        var lookPos = (direction - transform.position).normalized;
        lookPos.y = 0;
        var rotation = Quaternion.LookRotation(lookPos);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * speed);
    }
    
    private void RotateToBasedOnCorners() {
        if (navMeshAgent.path.corners.Length < 2) return;
        var index = 1;
        var lookPos = (navMeshAgent.path.corners[index] - transform.position).normalized;
        lookPos.y = 0;
        var rotation = Quaternion.LookRotation(lookPos);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * rotateSpeed);
    }
}
