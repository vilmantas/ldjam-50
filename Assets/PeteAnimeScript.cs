using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PeteAnimeScript : MonoBehaviour
{
    // Start is called before the first frame update
    public AudioSource source;
    
    void Update()
    {
        source.volume = GameManager.Instance.Volume * 0.5f;
    }
    
    // Start is called before the first frame update
    public void STEP()
    {
        source.Play();
    }
}
