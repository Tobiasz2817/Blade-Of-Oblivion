using UnityEngine;
using Panda;
using UnityEngine.AI;
using Unity.VisualScripting;
using System.Collections.Generic;
using MoreMountains.Feedbacks;

public class Tasks : MonoBehaviour
{
    GameObject player;
    NavMeshAgent _agent;
    Animator _animator;
    Vector3 point;
    [SerializeField]
    float waitTime = 3f;
    [SerializeField]
    List<Waypoint> _patrolWaypoints;
    [SerializeField]
    bool patroling =false;
    bool attacking;
    bool chasing;
    [SerializeField]
    bool waiting;
    int _patrolIndex;

    private void Start()
    {
        _agent = this.GetComponent<NavMeshAgent>();
        _animator = this.GetComponent<Animator>();
        player = GameObject.FindWithTag("Player");
        if(player == null)
        {
            Debug.LogError("There is no player on game scene");
        }
        if (_agent == null)
            Debug.LogError("NavmeshAgent is not attached to " + gameObject.name);
        else
        {
            if (_patrolWaypoints != null && _patrolWaypoints.Count >= 2)
            {
                patroling = true;
                _animator.SetBool("Patroling", patroling);
                _patrolIndex = 0;
            }
            else
                Debug.LogError("Not enough patrol points set to waypoint list");
        }
        if (_animator == null)
            Debug.LogError("Animator is not attached to " + gameObject.name);
        
    }
    [Task]
    bool PlayerInRange()
    {
        float distance = Vector3.Distance(transform.position, player.transform.position);
        if (distance <= 20f)
        {
            return true;
        }
        else return false;
    }
    [Task]
    void Waiting()
    {
        waitTime += Time.deltaTime;
        if (waitTime >= 5f)
        {
            waitTime = 0;
            patroling = true;
            return;
        }
        else
            _animator.SetBool("Patroling", false);
    }
    [Task]
    void SetPatrolPoint()
    {
        ChangePatrolPoint();
        Task.current.Succeed();
    }
    [Task]  
    void PatrolPoint()
    {
        if (ThisTask.isStarting)
        {
            patroling = true;
            SetAnimation();
            SetDestination();
            return;
        }
        if(patroling && _agent.remainingDistance <= 0f)
        {
            patroling = false;
            Task.current.Succeed();
        }
    }
    [Task]
    bool IsWaiting()
    {
        if (patroling)
            return false;
        else
            return true;
    }
    [Task]
    void EndWait()
    {
        waiting = false;
        ThisTask.Succeed();
    }
    [Task]
    void MoveToPlayer()
    {
        Vector3 direction = player.transform.position - transform.position;
        Vector3 offset = direction.normalized * 3f;
        Vector3 newdest = player.transform.position + offset;
        _agent.SetDestination(newdest);
    }
    void ChangePatrolPoint()
    {
        //Sprawdza czy patrolIndex mieœci sie w liœcie waypointów, jeœli nie to go zeruje
        _patrolIndex = (_patrolIndex + 1) % _patrolWaypoints.Count;
    }
    void SetAnimation()
    {
        _animator.SetBool("Patroling", patroling);
    }
    void SetDestination()
    {
        if (_patrolWaypoints != null)
        {
            Vector3 targetVector = _patrolWaypoints[_patrolIndex].transform.position;
            _agent.SetDestination(targetVector);
        }
    }
}