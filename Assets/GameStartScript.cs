using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStartScript : MonoBehaviour
{
    public LightningScript LightningScript;
    
    public MeshRenderer LightningBounds;

    public BlockerScript Blocker;

    public float TimeBetweenLightningStrikes;

    public float TimeTillStrike;
    
    // Update is called once per frame
    void Update()
    {
        TimeTillStrike -= Time.deltaTime;

        if (TimeTillStrike < 0)
        {
            TimeTillStrike = TimeBetweenLightningStrikes;

            var randomX = Random.Range(LightningBounds.bounds.min.x, LightningBounds.bounds.max.x);
            var randomZ = Random.Range(LightningBounds.bounds.min.z, LightningBounds.bounds.max.z);
            
            StartCoroutine(LightningScript.LightningToggle(new Vector3(randomX, 0, randomZ)));
        }
        
        if (!Input.GetKeyDown(KeyCode.Space)) return;


        StartCoroutine(Starter());

    }

    public IEnumerator Starter()
    {
        yield return Blocker.Hide();
        
        SceneManager.LoadScene("Scenes/Main");

        SceneManager.UnloadSceneAsync("Intro");
    }
}
