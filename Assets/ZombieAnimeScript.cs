using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieAnimeScript : MonoBehaviour
{
    public AudioSource source;
    
    void Update()
    {
        if (GameManager.Instance != null)
        {
            source.volume = GameManager.Instance.Volume;
        }
    }
    
    // Start is called before the first frame update
    public void SLAM()
    {
        source.Play();
        
        GameManager.Instance.Fort.TakeDamage(1);
    }
}
