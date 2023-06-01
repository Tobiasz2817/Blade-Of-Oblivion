using UnityEngine;
using UnityEngine.AI;

public abstract class Move : MonoBehaviour
{
    [SerializeField] private NavMeshAgent navMeshAgent;

    protected void MoveToPosition(Vector3 position) {
        navMeshAgent.SetDestination(position);
    }
}
