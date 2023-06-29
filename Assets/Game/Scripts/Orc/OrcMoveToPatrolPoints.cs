using Panda;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrcMoveToPatrolPoints : Move
{
    [SerializeField] private PatrolComponent patrolComponent;
    [SerializeField] private Wait waitComponent;
    [Task]
    public void TaskMoveToPatrolPoint()
    {
        if (patrolComponent.IsReachedDestination())
            if (waitComponent.corRunning)
                MoveToNextPoint();
            else
                waitComponent.waiting = true;
        MoveToCurrentPoint();
        Task.current.Succeed();
    }

    public void MoveToNextPoint()
    {
        MoveToPosition(patrolComponent.GetNextPoint().position);
    }

    public void MoveToCurrentPoint()
    {
        MoveToPosition(patrolComponent.GetCurrentPoint().position);
    }
}
