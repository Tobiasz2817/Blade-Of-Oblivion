using UnityEngine;
using Panda;
using UnityEngine.AI;
using Unity.VisualScripting;
using System.Collections.Generic;
using MoreMountains.Feedbacks;

public class Tasks : MonoBehaviour
{
    [Task]
    public bool Death()
    {
        if(this.GetComponent<Health>().GetHealth() <=0)
        {
            return true;
        }
        return false;

    }
    [Task]
    public void DisposeGameObject()
    {
        Destroy(gameObject);
    }
}