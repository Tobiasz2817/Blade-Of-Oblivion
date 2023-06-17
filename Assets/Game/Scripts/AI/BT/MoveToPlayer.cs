using Panda;

public class MoveToPlayer : Move
{

    [Task]
    public void TaskMoveToPlayer() {
        MoveToPosition(PlayerSingleton.Instance.GetPosition().position);
        Task.current.Succeed();
    }
}
