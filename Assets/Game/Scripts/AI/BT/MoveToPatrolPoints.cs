using System;
using UnityEngine;
using Task = Panda.Task;

public class MoveToPatrolPoints : Move
{
    [SerializeField] private PatrolComponent patrolComponent;

    private FirstEntryBT firstEntryBt;
    private void Start() {
        firstEntryBt = new FirstEntryBT();
    }

    [Task]
    public void TreeMoveTask() {
        if(patrolComponent.IsReachedDestination())
            MoveToNextPoint();

        if (firstEntryBt.IsFirstEntry())
            MoveToNextPoint();

        Task.current.Succeed();
    }
    
    public void MoveToNextPoint() {
        MoveToPosition(patrolComponent.GetNextPoint().position);
    }
}

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
