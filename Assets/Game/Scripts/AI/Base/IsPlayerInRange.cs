using Panda;
using UnityEngine;

public class IsPlayerInRange : MonoBehaviour
{
    [Task]
    public bool TaskPlayerIsInRange(float range) {
        if (PlayerSingleton.Instance && Vector3.Distance(PlayerSingleton.Instance.GetPosition().position,transform.position) < range) {
            Task.current.Succeed();
            return true;
        }

        Task.current.Fail();
        return false;
    }
}
