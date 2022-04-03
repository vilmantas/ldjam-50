using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

public class InterractableScirpt : MonoBehaviour
{
    public GameObject ResourcePrefab;

    public void Die()
    {
        for (int i = 0; i < Random.Range(1, 4); i++)
        {
            var random = Random.insideUnitCircle;
            
            var random_pos = random * 3f;

            var curr_pos = transform.position;

            var spawn_pos = new Vector3(curr_pos.x + random_pos.x, 0, transform.position.z + random_pos.y);

            Instantiate(ResourcePrefab, spawn_pos, Quaternion.identity);
        }
        
        DestroyImmediate(gameObject);
    }
}
