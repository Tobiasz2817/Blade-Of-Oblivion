using UnityEngine;
using UnityEngine.AI;

public abstract class Move : MonoBehaviour
{
    [SerializeField] private NavMeshAgent navMeshAgent;
    [SerializeField] private DestinationPathController destinationPathController;

    protected void MoveToPosition(Vector3 position) {
        //destinationPathController.TrySetNewDestination(position);
        navMeshAgent.SetDestination(position);
    }

    protected void BreakMovement() {
        navMeshAgent.ResetPath();
    }
}

