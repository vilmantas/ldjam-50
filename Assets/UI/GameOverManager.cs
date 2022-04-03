using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Mime;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class GameOverManager : MonoBehaviour
{
    public GameManager Manager;

    public TextMeshProUGUI Text;
    public TextMeshProUGUI Text2;
    public TextMeshProUGUI Text3;

    public Image background;

    public bool Animating = false;

    // Update is called once per frame
    void OnEnable()
    {
        if (Manager.GameOver)
        {
            if (!Animating)
            {
                Animating = true;
                StartCoroutine(AnimateGameOver());    
            }
        }
    }

    public IEnumerator AnimateGameOver()
    {
        var Rekt = background.GetComponent<RectTransform>();
        
        var rect = Rekt.rect;
        
        Text.color = new Color(Text.color.r, Text.color.g, Text.color.b ,0);
        Text2.color = new Color(Text2.color.r, Text2.color.g, Text2.color.b ,0);
        Text3.color = new Color(Text2.color.r, Text2.color.g, Text2.color.b ,0);
        
        Rekt.sizeDelta = new Vector2(0, 10);

        background.gameObject.SetActive(true);
        
        yield return StartCoroutine(AnimateBackgroundWidth(rect.width));
        
        yield return StartCoroutine(AnimateBackgroundHeight(rect.height));
        
        Text.gameObject.SetActive(true);
        
        yield return StartCoroutine(AnimateTextOpacity());
    }

    public int IterationsCount = 15;
    
    public IEnumerator AnimateBackgroundWidth(float width)
    {
        var Rekt = background.GetComponent<RectTransform>();

        for (int i = 1; i <= IterationsCount; i++)
        {
            var percentDone = i / (float)IterationsCount;
        
            var newSize = Mathf.Lerp(0, width, percentDone);

            Rekt.sizeDelta = new Vector2(newSize, Rekt.sizeDelta.y);
            
            yield return new WaitForSeconds(0.0025f);
        }
    }
    
    public IEnumerator AnimateBackgroundHeight(float height)
    {
        var Rekt = background.GetComponent<RectTransform>();
        
        for (int i = 1; i <= IterationsCount; i++)
        {
            var percentDone = i / (float)IterationsCount;
        
            var newSize = Mathf.Lerp(0, height, percentDone);

            Rekt.sizeDelta = new Vector2(Rekt.sizeDelta.x, newSize);

            yield return new WaitForSeconds(0.0025f);
        }
    }
    
    public IEnumerator AnimateTextOpacity()
    {
        for (int i = 1; i <= IterationsCount; i++)
        {
            var percentDone = i / (float)IterationsCount;
        
            var newColor = Mathf.Lerp(0, 1, percentDone);

            Text.color = new Color(Text.color.r, Text.color.g, Text.color.b ,newColor);
            Text2.color = new Color(Text2.color.r, Text2.color.g, Text2.color.b ,newColor);
            Text3.color = new Color(Text3.color.r, Text3.color.g, Text3.color.b ,newColor);
            
            yield return new WaitForSeconds(0.0025f);
        }
    }
}
