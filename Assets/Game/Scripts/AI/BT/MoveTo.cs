
using Panda;
using UnityEngine;
using UnityEngine.AI;

public class MoveTo : MonoBehaviour
{
    [SerializeField] private Transform moveTo;
    [SerializeField] private NavMeshAgent agent;
    
    [Task]
    public void MoveToPoint() {
        agent.SetDestination(moveTo.position);
    }
}
