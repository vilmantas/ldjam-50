using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

public class InterractableScirpt : MonoBehaviour
{
    public GameObject Mesh;
    
    public AudioSource OpenCrateSource;

    public AudioSource DropItemSource;
    
    public GameObject ResourcePrefab;

    public bool IsDead = false;
    
    void Update()
    {
        OpenCrateSource.volume = GameManager.Instance.Volume;
        DropItemSource.volume = GameManager.Instance.Volume;
    }
    
    public void Die()
    {
        if (IsDead) return;
        
        IsDead = true;
        
        Mesh.SetActive(false);
        
        OpenCrateSource.Play();

        
        for (int i = 0; i < Random.Range(1, 4); i++)
        {
            StartCoroutine(SpawnItem());
        }
        
        Destroy(gameObject, 0.3f);
    }

    private IEnumerator SpawnItem()
    {
        yield return new WaitForSeconds(Random.Range(0.05f, 0.2f));
        
        var random = Random.insideUnitCircle;
            
        var random_pos = random * 3f;

        var curr_pos = transform.position;

        var spawn_pos = new Vector3(curr_pos.x + random_pos.x, 0, transform.position.z + random_pos.y);

        DropItemSource.Play();
        Instantiate(ResourcePrefab, spawn_pos, Quaternion.identity);
    }
}
