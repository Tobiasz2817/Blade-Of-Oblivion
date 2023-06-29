using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Task = Panda.Task;

public class Wait : MonoBehaviour
{
    public bool waiting;
    public bool corRunning;
    private Animator _animator;
    [SerializeField] private PatrolComponent patrol;
    private void Awake()
    {
        _animator = this.GetComponent<Animator>();
    }

    private void Start()
    {
        waiting = false;
        corRunning = false;
    }
    private void Update()
    {
        if (patrol.IsReachedDestination() && !corRunning)
            waiting = true;
        if(waiting && !corRunning)
        {
            StartCoroutine(ChangeWaitingAfterDelay(2f));
        }
    }
    private IEnumerator ChangeWaitingAfterDelay(float delay)
    {
        corRunning = true;
        yield return new WaitForSeconds(delay);
        waiting = false;
        yield return new WaitForSeconds(0.5f);
        corRunning = false;
        Debug.Log("koniec czekania");
    }
    [Task]
    public bool IsWaiting()
    {
        return waiting;
    }

    [Task]
    public void Waiting()
    {
        Task.current.Succeed();
    }
}
