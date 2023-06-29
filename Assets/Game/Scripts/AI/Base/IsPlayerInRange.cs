using Panda;
using UnityEngine;

public class IsPlayerInRange : MonoBehaviour
{
    [SerializeField] private BoxCollider range;
    [SerializeField] private MoveToPlayer moveToPlayer;
    
    
    [Task]
    public bool TaskPlayerIsInObjRange() {
        if (PlayerSingleton.Instance) {
            float hight = range.size.x /2;  
            float widht = range.size.z /2;
            var enemy = PlayerSingleton.Instance.GetPosition();
            if (enemy.transform.position.x > range.transform.position.x + -hight &&  enemy.transform.position.x < range.transform.position.x + hight && enemy.transform.position.z > range.transform.position.z + -widht && enemy.transform.position.z < range.transform.position.z + widht) 
            { 
                Task.current.Succeed();
                return true;
            }
        }

        Task.current.Fail();
        return false;
    }
    
    [Task]
    public bool TaskPlayerIsInRange(float range) {
        if(moveToPlayer != null)
            moveToPlayer.breakDistance = range;
        if (PlayerSingleton.Instance && Vector3.Distance(PlayerSingleton.Instance.GetPosition().position,transform.position) < range) {
            Task.current.Succeed();
            return true;
        }

        Task.current.Fail();
        return false;
    }
    
    
    [Task]
    public bool TaskPlayerIsInRangeBool(float range) {
        if(moveToPlayer != null)
            moveToPlayer.breakDistance = range;
        if (PlayerSingleton.Instance && Vector3.Distance(PlayerSingleton.Instance.GetPosition().position,transform.position) < range) {
            return true;
        }

        return false;
    }
    
    [Task]
    public void Test() {
       Task.current.Succeed();
    }
}
