using Panda;
using UnityEngine;

public class MoveToPlayer : Move
{
    [SerializeField] public float breakDistance = 3f;
    
    [Task]
    public void TaskMoveToPlayer() {
        var playerPos = PlayerSingleton.Instance.GetPosition().position;
        if (IsReachedDestination(playerPos)) BreakMovement();
        else MoveToPosition(playerPos);
        
        Task.current.Succeed();
    }
    
    public bool IsReachedDestination(Vector3 direction) {
        if (Vector3.Distance(direction, transform.position) < breakDistance)
            return true;

        return false;
    }
}
