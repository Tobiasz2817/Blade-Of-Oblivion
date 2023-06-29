using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

public class DestinationPathController : MonoBehaviour
{
    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private LayerMask layerMask;
    [SerializeField] private float breakDistance;
    
    private Vector3 direction;
    private List<Vector3> cornersList = new List<Vector3>();
    private int currentIndex = 0;

    public bool TrySetNewDestination(Vector3 newDirection) {
        if (direction == newDirection) return false;
        this.direction = newDirection;
        agent.SetDestination(direction);
        StopAllCoroutines();
        StartCoroutine(MoveToPathCorners());

        agent.path = new NavMeshPath();
        
        return true;
    }

    private IEnumerator MoveToPathCorners() {
        yield return new WaitForSeconds(0.01f);
        cornersList = agent.path.corners.ToList();
        agent.ResetPath();
        SetIndex(0);
        Debug.Log(cornersList.Count);
        if(!TryIncrementIndex())
            yield break;
        
        var index = GetIndex();
        agent.SetDestination(cornersList[index]);
        
        while (true) {
            yield return new WaitUntil(() => GetDistance(cornersList[GetIndex()],agent.transform.position,layerMask,agent.path) < breakDistance);

            if(!TryIncrementIndex())
                yield break;

            agent.SetDestination(cornersList[GetIndex()]);
        }

    }

    public bool IsIndexOutOfRange(int index) {
        return cornersList.Count - 1 > index ? true : false;
    }
    
    public bool IsLastIndex(int index) {
        return cornersList.Count - 1 == index ? true : false;
    }
    
    public int GetIndex() {
        return currentIndex;
    }

    public bool TryIncrementIndex() {
        var index = GetIndex() + 1;

        if (currentIndex < cornersList.Count - 1) {
            SetIndex(index);
            return true;
        }

        return false;
    }
    
    public void SetIndex(int newIndex) {
        currentIndex = newIndex;
    }

    public bool IsReachedDestination(Vector3 direction, float breakDistance) {
        if (Vector3.Distance(direction, agent.transform.position) < breakDistance)
            return true;

        return false;
    }
    
    public float GetDistance(Vector3 fromPosition, Vector3 toPosition, LayerMask layer, NavMeshPath navMeshPath) 
    { 
        if (NavMesh.CalculatePath(fromPosition, toPosition, layer, navMeshPath)) 
        { 
            float distance = Vector3.Distance(fromPosition, navMeshPath.corners[0]); 
            for (int i = 1; i < navMeshPath.corners.Length; i++) 
            { 
                distance += Vector3.Distance(navMeshPath.corners[i - 1], navMeshPath.corners[i]); 
            } 
            return distance; 
        } 
        
        return 0f; 
    }
}