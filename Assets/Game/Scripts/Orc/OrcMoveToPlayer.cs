using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrcMoveToPlayer : Move
{
    [Panda.Task]
    public void TaskMoveToPlayer()
    {
        MoveToPosition(PlayerSingleton.Instance.GetPosition().position);
        if(Vector3.Distance(PlayerSingleton.Instance.GetPosition().position,this.transform.position) <= 5f)
            Panda.Task.current.Succeed();
    }
}
