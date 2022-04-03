using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    
    public LightningScript Lightning;
    
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

    public List<ZombieScript> Zombies = new List<ZombieScript>();

    public int Wave = 1;
    
    public bool GameOver => !Fort.IsAlive;

    private void Awake()
    {
        Instance = this;
    }

    public int Volume = 1;

    public float TimeBetweenLightningStrikes = 20f;

    public float NextLightningStrike = 9990f;
    
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            Volume = Volume == 1 ? 0 : 1;
        }
        
        if (GameOver) return;

        NextLightningStrike -= Time.deltaTime;

        if (NextLightningStrike < 0)
        {
            NextLightningStrike = TimeBetweenLightningStrikes + Random.Range(-Wave, 0);

            var walkingDead = Zombies.Where(x => !x.IsDead).ToArray();

            if (walkingDead.Length > 0)
            {
                SetDeadZombie(walkingDead[Random.Range(0, walkingDead.Length - 1)]);
            }
        }
        
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
        Zombies.ForEach(KillZombie);

        yield return new WaitForSeconds(1f);
        
        Zombies.Clear();
    }

    public void SetDeadZombie(ZombieScript zombie)
    {
        StartCoroutine(Lightning.LightningToggle(zombie.transform.position));
        
        zombie.SetDead();
    }

    public void KillZombie(ZombieScript zombie)
    {
        StartCoroutine(DelayedDestroy(Random.Range(0.1f, 0.8f), zombie));
    }

    public void SpawnZombies()
    {
        var zombieCount = Random.Range(Wave + 1, Wave + 2);
        
        for (int i = 0; i < zombieCount; i++)
        {
            var spawnPoint = SpawnPoints[Random.Range(0, SpawnPoints.Length - 1)].Point;

            var instance = Instantiate(ZombiePrefab, new Vector3(spawnPoint.position.x, 0, spawnPoint.position.z), Quaternion.identity);

            var zmb = instance.GetComponent<ZombieScript>();
            
            instance.SetActive(false);

            zmb.Priority = i;
            
            Zombies.Add(zmb);

            StartCoroutine(DelayedEnable(Random.Range(1f, 1.4f), instance));
        }
    }

    public IEnumerator DelayedEnable(float delay, GameObject instance)
    {
        yield return new WaitForSeconds(delay);
        
        instance.SetActive(true);
    }
    
    public IEnumerator DelayedDestroy(float delay, ZombieScript instance)
    {
        yield return new WaitForSeconds(delay);

        if (!instance.IsDead)
        {
            SetDeadZombie(instance);
        }
        
        Destroy(instance.gameObject);
    }
}

[Serializable]
public class CycleChangedEvent : UnityEvent<GameManager> {}