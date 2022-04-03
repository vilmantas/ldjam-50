using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour
{
    public ZombieSpawnScript[] SpawnPoints;

    public Transform ZombieSpawnArea;

    public GameObject ZombiePrefab;
    
    public PlayerScript Player;

    public FortScript Fort;
    
    public float DayDuration = 30f;

    public float NightDuration = 30f;

    public float CurrentDuration = 30f;
    
    public float TimeLeft = 30f;

    public bool IsDay = true;

    public CycleChangedEvent CycleChanged;

    public List<GameObject> Zombies = new List<GameObject>();

    public int Wave = 1;
    
    public bool GameOver => !Fort.IsAlive;
    
    // Update is called once per frame
    void Update()
    {
        if (GameOver) return;
        
        TimeLeft -= Time.deltaTime;

        if (!(TimeLeft <= 0)) return;
        
        IsDay = !IsDay;

        TimeLeft = IsDay ? DayDuration : NightDuration + Wave;

        CurrentDuration = IsDay ? TimeLeft : TimeLeft + Wave;
        
        CycleChanged.Invoke(this);

        if (!IsDay)
        {
            SpawnZombies();            
        }
        else
        {
            Wave++;
            StartCoroutine(KillZombies());
        }
        
    }

    public IEnumerator KillZombies()
    {
        Zombies.ForEach(x => StartCoroutine(DelayedDestroy(Random.Range(0.1f, 0.8f), x)));

        yield return new WaitForSeconds(1f);
        
        Zombies.Clear();
    }

    public void SpawnZombies()
    {
        for (int i = 0; i < Random.Range(Wave, Wave * 2); i++)
        {
            var spawnPoint = SpawnPoints[Random.Range(0, SpawnPoints.Length - 1)].Point;

            var instance = Instantiate(ZombiePrefab, new Vector3(spawnPoint.position.x, 0, spawnPoint.position.z), Quaternion.identity);

            var zmb = instance.GetComponent<ZombieScript>();
            
            instance.SetActive(false);

            zmb.Priority = i;
            
            Zombies.Add(instance);

            StartCoroutine(DelayedEnable(Random.Range(1f, 1.4f), instance));
        }
    }

    public IEnumerator DelayedEnable(float delay, GameObject instance)
    {
        yield return new WaitForSeconds(delay);
        
        instance.SetActive(true);
    }
    
    public IEnumerator DelayedDestroy(float delay, GameObject instance)
    {
        yield return new WaitForSeconds(delay);
        
        Destroy(instance);
    }
}

[Serializable]
public class CycleChangedEvent : UnityEvent<GameManager> {}