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
    private int currentIndex = -1;

    public bool TrySetNewDestination(Vector3 newDirection) {
        if (direction == newDirection) return false;
        this.direction = newDirection;
        agent.SetDestination(direction);
        StartCoroutine(ta());

        return true;
    }

    private IEnumerator ta() {
        
        yield return new WaitForSeconds(0.01f);
        if (agent.hasPath) {
            Debug.Log("YES");
        }

        cornersList = agent.path.corners.ToList();
        agent.ResetPath();

        foreach (var VARIABLE in cornersList) {
            Debug.Log(VARIABLE);
        }

        agent.SetDestination(cornersList[currentIndex++]);
    }

    private void Update() {
        if (cornersList.Count <= 0) return;
        
        if (agent.hasPath) {
            if (TryIncrementIndex()) {
                agent.SetDestination(cornersList[currentIndex]);
                Debug.Log("X");
            }
            else 
                cornersList.Clear();
        }

        /*
        if (GetDistance(cornersList[currentIndex], agent.transform.position, layerMask, agent.path) < breakDistance) {

        }*/
    }

    public bool TryIncrementIndex() {
        var index = currentIndex + 1;
        Debug.Log(index);
        Debug.Log(cornersList.Count);

        if (currentIndex < cornersList.Count - 1) {
            currentIndex = index;
            return true;
        }

        return false;
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