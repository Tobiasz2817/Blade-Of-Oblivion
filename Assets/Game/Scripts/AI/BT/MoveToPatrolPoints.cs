using System;
using UnityEngine;
using Task = Panda.Task;

public class MoveToPatrolPoints : Move
{
    [SerializeField] private PatrolComponent patrolComponent;

    [Task]
    public void TaskMoveToPatrolPoint() {
        if(patrolComponent.IsReachedDestination())
            MoveToNextPoint();

        MoveToCurrentPoint();
        Task.current.Succeed();
    }
    
    public void MoveToNextPoint() {
        MoveToPosition(patrolComponent.GetNextPoint().position);
    }
    
    public void MoveToCurrentPoint() {
        MoveToPosition(patrolComponent.GetCurrentPoint().position);
    }
}

/*
public class FirstEntryBT
{
    private bool firstEntry = true;

    public bool IsFirstEntry() {
        if (firstEntry) {
            firstEntry = false;
            return true;
        }
        return firstEntry;
    }
}
*/
