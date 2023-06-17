using System.Collections.Generic;
using UnityEngine;

public class PatrolComponent : MonoBehaviour
{
    [SerializeField] private List<Transform> points = new List<Transform>();
    [SerializeField] private float distance = 1f;

    private int currentIndexPoint = 0;

    
    public Transform GetNextPoint() {
        return points[GetNextIndexPoint(currentIndexPoint)];
    }

    public Transform GetCurrentPoint() {
        return points[currentIndexPoint];
    }
    
    private int GetNextIndexPoint(int currentIndex) {
        currentIndex++;
        if (currentIndex >= points.Count)
            currentIndex = 0;

        currentIndexPoint = currentIndex;
        return currentIndexPoint;
    }
    
    public bool IsReachedDestination() {
        if (Vector3.Distance(points[currentIndexPoint].position, transform.position) < distance)
            return true;

        return false;
    }
}
