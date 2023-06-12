using UnityEngine;
using UnityEngine.AI;

public class AnimateMovementByNavMesh : MonoBehaviour
{
    [SerializeField] private string moveX = "MoveX";
    [SerializeField] private string moveZ = "MoveZ";

    [SerializeField] private Animator animator;
    [SerializeField] private NavMeshAgent navMeshAgent;

    void Update() {
        Vector3 dest = (navMeshAgent.destination - transform.position).normalized;
        animator.SetFloat(moveZ,Vector3.Dot(dest, transform.forward));
        animator.SetFloat(moveX,Vector3.Dot(dest, transform.right));
    }
}
