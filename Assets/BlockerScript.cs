using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BlockerScript : MonoBehaviour
{
    public GameObject IntroStuff;

    public GameObject Instructions;

    public Image Blocker;
    
    // Start is called before the first frame update
    void Start()
    {
        // Instructions.SetActive(true);
        // IntroStuff.SetActive(true);

        Blocker.enabled = true;
        StartCoroutine(Reveal());
    }

    public IEnumerator Reveal()
    {
        yield return new WaitForSeconds(1);
        
        for (int i = 1; i <= 50; i++)
        {
            var percentDone = i / (float)50;
        
            var newColor = Mathf.Lerp(1, 0, percentDone);

            Blocker.color = new Color(Blocker.color.r, Blocker.color.g, Blocker.color.b ,newColor);
            
            yield return new WaitForSeconds(0.005f);
        }
    }
    
    public IEnumerator Hide()
    {
        for (int i = 1; i <= 50; i++)
        {
            var percentDone = i / (float)50;
        
            var newColor = Mathf.Lerp(0, 1, percentDone);

            Blocker.color = new Color(Blocker.color.r, Blocker.color.g, Blocker.color.b ,newColor);
            
            yield return new WaitForSeconds(0.005f);
        }
    }
    
    // Update is called once per frame
    void Update()
    {
        
    }
}
