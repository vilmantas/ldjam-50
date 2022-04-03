using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStartScript : MonoBehaviour
{

    public BlockerScript Blocker;
    
    // Update is called once per frame
    void Update()
    {
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
