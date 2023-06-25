using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Panda;
using UnityEngine.WSA;

public class BossTriggers : MonoBehaviour
{
    [SerializeField] bool activated = false;
    [Task]
    public bool HealthTrigger()
    {
        if (this.GetComponent<Health>().GetHealth() < (this.GetComponent<Health>().GetMaxHealth() / 2) && !activated)
        {
            Task.current.Succeed();
            return true;
        }
        else
            return false;
    }
    [Task]
    public void SummonHelper()
    {
        GameObject helper = GameObject.FindGameObjectWithTag("BossHelper");
        helper.gameObject.GetComponent<ActivateHelper>().SetActivate();
        activated = true;
    }    
}
