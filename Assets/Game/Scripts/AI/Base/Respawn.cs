using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Respawn : MonoBehaviour
{
    [SerializeField] List<Transform> spawnPoints; 
    CombatManager combatManager;
    CharacterMovement characterMovement;
    Animator animator;
    Health health;
    bool corRunning;
    private void Awake()
    {
        animator = this.GetComponentInChildren<Animator>();
        health = this.GetComponent<Health>();
        combatManager = GetComponent<CombatManager>();
        characterMovement = GetComponent<CharacterMovement>();
    }
    private void Update()
    {
        if (health.GetHealth() <= 0 && !corRunning)
        {
            corRunning = true;
            characterMovement.enabled = false;
            combatManager.enabled = false;
            //Debug.Log("Zginolesi");
            StartCoroutine(Die());
        }
    }
    IEnumerator Die()
    {
        animator.Play("Death");
        //Debug.Log("Umar�e�, spr�buj jeszcze raz");
        yield return new WaitForSeconds(5f);
        Resurect();
        corRunning = false;
        animator.Play("Walk");
    }
    void Resurect()
    {
        this.transform.position = GetResurectPosition();
        characterMovement.enabled = true;
        combatManager.enabled = true;
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
