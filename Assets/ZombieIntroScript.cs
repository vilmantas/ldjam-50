using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public class ZombieIntroScript : MonoBehaviour
{
    public Animator Animator;
    
    public NavMeshAgent Agent;

    public MeshRenderer RoamingBounds;

    public float TimeTillNextMove = 10f;

    public float TimeBetweenMoves = 10f;

    private void Start()
    {
        TimeTillNextMove = Random.Range(1f, 3f);
    }

    // Update is called once per frame
    void Update()
    {
        if (Agent.velocity == Vector3.zero)
        {
            Animator.SetFloat("Speed", 0);
        }
        else
        {
            Animator.SetFloat("Speed", 1);
        }

        
        TimeTillNextMove -= Time.deltaTime;

        if (!(TimeTillNextMove < 0)) return;
        
        TimeTillNextMove = TimeBetweenMoves + Random.Range(-2f, 0);
            
        var randomX = Random.Range(RoamingBounds.bounds.min.x, RoamingBounds.bounds.max.x);
        var randomZ = Random.Range(RoamingBounds.bounds.min.z, RoamingBounds.bounds.max.z);

        Agent.destination = new Vector3(randomX, 0, randomZ);
    }
}
