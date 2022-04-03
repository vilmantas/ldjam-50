using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public class ZombieScript : MonoBehaviour
{
    public GameManager Manager;

    public NavMeshAgent Agent;

    public Animator Animator;

    public int Priority = 0;

    public float AttackDelay = 1f;

    public bool Attacking = false;

    private void Update()
    {
        if (Manager.GameOver) return;

        if (Animator == null) return;

        if (Agent == null) return;
        
        if (Agent.velocity == Vector3.zero)
        {
            Animator.SetFloat("Speed", 0);
        }
        else
        {
            Animator.SetFloat("Speed", 1);
        }

    }

    private void OnEnable()
    {
        Animator = GetComponentInChildren<Animator>();
        
        Agent = GetComponent<NavMeshAgent>();
        
        Manager = GameObject.Find("GameManager").GetComponent<GameManager>();
        
        var bounds = Manager.Fort.Bounds.GetComponentInChildren<Renderer>().bounds;

        var randomX = Random.Range(bounds.min.x, bounds.max.x);
        var randomZ = Random.Range(bounds.min.z, bounds.max.z);

        Agent.avoidancePriority = Priority;
        Agent.destination = new Vector3(randomX, 0, randomZ);
    }

    private void OnTriggerEnter(Collider other)
    {
        print("ENTERED");
        
        if (Agent == null) return;

        if (!other.CompareTag("Fort")) return;
        
        var target = Agent.destination;

        Agent.isStopped = true;
            
        transform.LookAt(new Vector3(target.x, transform.position.y, target.z));

        if (!Attacking)
        {
            Attacking = true;
            
            Animator.SetTrigger("Attacking");
        
            StartCoroutine(Attack());    
        }
    }

    private IEnumerator Attack()
    {
        while (true)
        {
            Manager.Fort.TakeDamage(1);

            yield return new WaitForSeconds(AttackDelay);

            if (Manager.GameOver) yield break;
        }
    }
}
