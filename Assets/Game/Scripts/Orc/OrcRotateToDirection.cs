using Panda;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class OrcRotateToDirection : MonoBehaviour
{
    [SerializeField] private NavMeshAgent navMeshAgent;
    [SerializeField] private float rotateSpeed = 15f;

    private void OnEnable()
    {
        navMeshAgent.updateRotation = false;
    }
    private void OnDisable()
    {
        navMeshAgent.updateRotation = true;
    }

    [Task]
    public void RotateToPlayer()
    {
        if (!PlayerSingleton.Instance)
        {
            Task.current.Fail();
            return;
        }
        var lookPos = (PlayerSingleton.Instance.GetPosition().position - transform.position).normalized;
        lookPos.y = 0;
        var rotation = Quaternion.LookRotation(lookPos);
        RotateTo(PlayerSingleton.Instance.GetPosition().position);
        if (Quaternion.Angle(this.transform.rotation, rotation) <= 7f)
            Task.current.Succeed();
    }

    [Task]
    public void RotateToNavMeshDestination()
    {
        RotateTo(navMeshAgent.destination);
        Task.current.Succeed();
    }

    private void RotateTo(Vector3 direction)
    {
        var lookPos = (direction - transform.position).normalized;
        lookPos.y = 0;
        var rotation = Quaternion.LookRotation(lookPos);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * rotateSpeed);
    }
}
