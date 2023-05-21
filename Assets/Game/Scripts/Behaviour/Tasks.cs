using UnityEngine;
using Panda;
using UnityEngine.AI;

public class Tasks : MonoBehaviour
{
    NavMeshAgent agent;
    Animator animator;
    Vector3 point;
    private void Start()
    {
        agent = this.GetComponent<NavMeshAgent>();
        animator = this.GetComponent<Animator>();
    }
    private void Update()
    {
        if (transform.position != point)
        {
            animator.SetBool("Partoling", true);
        }
        else animator.SetBool("Partoling", false);
    }
    [Task]
    void PatrolPoint()
    {
        point = RandomPoint(agent.transform.position,10.0f);
        Debug.Log(point);
        MoveTo(point);
        ThisTask.Complete(true);
    }
    public static Vector3 RandomPoint(Vector3 center, float maxDistance)
    {
        // Get Random Point inside Sphere which position is center, radius is maxDistance
        Vector3 randomPos = Random.insideUnitSphere * maxDistance + center;

        NavMeshHit hit; // NavMesh Sampling Info Container

        // from randomPos find a nearest point on NavMesh surface in range of maxDistance
        NavMesh.SamplePosition(randomPos, out hit, maxDistance, NavMesh.AllAreas);

        return hit.position;
    }
    void MoveTo(Vector3 point)
    {
        Vector3 pos = this.gameObject.transform.position;
        agent.SetDestination(point);
    }
}
