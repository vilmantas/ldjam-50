using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

public class LightningScript : MonoBehaviour
{
    public GameObject Lightning;
    
    public AudioSource source;

    // Update is called once per frame
    void Update()
    {
        if (GameManager.Instance != null)
            source.volume = GameManager.Instance.Volume;
    }
    
    public IEnumerator LightningToggle(Vector3 position)
    {
        Lightning.SetActive(true);
        source.Play();
        var v = position + Vector3.up * 0.5f;

        transform.position = v;
        
        Lightning.transform.rotation = Quaternion.Euler(0, Random.rotation.eulerAngles.y, 0);

        yield return new WaitForSeconds(0.01f);
        
        Lightning.SetActive(false);
    }
}
