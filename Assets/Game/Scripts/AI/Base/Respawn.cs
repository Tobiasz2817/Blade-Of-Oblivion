using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Respawn : MonoBehaviour
{
    [SerializeField] List<Transform> spawnPoints;
    Animator animator;
    Health health;
    bool corRunning;
    private void Awake()
    {
        animator = this.GetComponentInChildren<Animator>();
        health = this.GetComponent<Health>();
    }
    private void Update()
    {
        if (health.GetHealth() <= 0 && !corRunning)
        {
            corRunning = true;
            Debug.Log("Zginolesi");
            StartCoroutine(Die());
        }
    }
    IEnumerator Die()
    {
        animator.Play("Death");
        Debug.Log("Umar³eœ, spróbuj jeszcze raz");
        yield return new WaitForSeconds(5f);
        Resurect();
        corRunning = false;
        animator.Play("Walk");
    }
    void Resurect()
    {
        this.transform.position = GetResurectPosition();
        health.ExpandCurrentHealth(health.GetMaxHealth());
    }
    Vector3 GetResurectPosition()
    {
        Vector3 spot = transform.position;
        float distance = Mathf.Infinity;
        foreach (Transform spawn in spawnPoints)
            if (Vector3.Distance(spawn.transform.position, this.transform.position) < distance)
            {
                distance = Vector3.Distance(spawn.transform.position, this.transform.position);
                spot = spawn.transform.position;
            }
        return spot;
    }
}
