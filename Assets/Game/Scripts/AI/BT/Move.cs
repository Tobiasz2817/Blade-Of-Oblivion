using UnityEngine;
using UnityEngine.AI;

public abstract class Move : MonoBehaviour
{
    [SerializeField] private NavMeshAgent navMeshAgent;
    [SerializeField] private DestinationPathController destinationPathController;
    public AgentType type;

    protected void MoveToPosition(Vector3 position) {
        switch (type) {
            case AgentType.Basic:
                destinationPathController.TrySetNewDestination(position);
                break;
            case AgentType.Boss:
                navMeshAgent.SetDestination(position);
                break;
        }
    }

    protected void BreakMovement() {
        navMeshAgent.ResetPath();
    }
}

public enum AgentType
{
    Boss,
    Basic
}
