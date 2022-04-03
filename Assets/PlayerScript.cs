using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerScript : MonoBehaviour
{
    public GameObject Fort;
    
    public LayerMask InterractableLayer;

    public GameObject ResourceParent;

    public ResourceComponent Resource;

    public Transform FortEntrance;

    public GameManager Manager;

    public NavMeshAgent Agent;

    public Animator AnimatorController;
    
    private void Start()
    {
        Manager = GameObject.Find("GameManager").GetComponent<GameManager>();

        Agent = GetComponent<NavMeshAgent>();
        
        AnimatorController = GetComponentInChildren<Animator>();
    }

    void Update()
    {
        if (Agent.velocity == Vector3.zero)
        {
            AnimatorController.SetFloat("Speed", 0);
        }
        else
        {
            AnimatorController.SetFloat("Speed", 1);
        }
        
        if (!Manager.IsDay)
        {
            AnimatorController.SetBool("Crying", true);
        }
        else
        {
            AnimatorController.SetBool("Crying", false);
        }


        
        if (Manager.GameOver) return;
        
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (Resource != null)
            {
                Resource.transform.SetParent(null);

                var random = UnityEngine.Random.insideUnitCircle;
            
                var random_pos = random * 1.5f;

                var curr_pos = transform.position;

                var spawn_pos = new Vector3(curr_pos.x + random_pos.x, 0, transform.position.z + random_pos.y);

                Resource.transform.position = spawn_pos;
                
                Resource = null;
            }
        }
        
        if (!Input.GetKeyDown(KeyCode.Space)) return;
        
        var hits = Physics.OverlapSphere(transform.position, 3f, InterractableLayer);
        
        if (hits.Length <= 0)
        {
            return;
        }

        foreach (var raycastHit in hits)
        {
            var i = raycastHit.GetComponent<InterractableScirpt>();

            if (i == null) continue;

            GetComponent<UnityEngine.AI.NavMeshAgent>().destination = transform.position;
            transform.LookAt(new Vector3(raycastHit.transform.position.x, transform.position.y,
                raycastHit.transform.position.z));

            i.Die();

            break;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Resource") && Resource == null)
        {
            Resource = other.gameObject.GetComponent<ResourceComponent>();

            other.transform.SetParent(ResourceParent.transform);
            
            other.transform.localPosition = Vector3.zero;
        }
    }

    public void DayCycleChanged(GameManager manager)
    {
        if (manager.IsDay)
        {
            GetComponent<NavMeshAgent>().destination = FortEntrance.position;
        }
        else
        {
            GetComponent<NavMeshAgent>().destination = Fort.transform.position;
        }
    }

    public IEnumerator DelayedMove(Vector3 dest, float delay)
    {
        GetComponent<NavMeshAgent>().destination = dest;

        yield break;
    }
}
