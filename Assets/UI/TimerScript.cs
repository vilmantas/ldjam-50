using System.Collections;
using System.Collections.Generic;
using System.Net.Mime;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;
using Image = UnityEngine.UI.Image;

public class TimerScript : MonoBehaviour
{
    public GameManager Manager;

    public RectTransform Rekt;

    public Image Im;

    public bool ChangeColor = true;

    public float StartingWidth;
    
    // Start is called before the first frame update
    void Start()
    {
        Rekt = GetComponent<RectTransform>();

        Im = GetComponent<Image>();

        StartingWidth = Rekt.rect.width;
    }

    // Update is called once per frame
    void Update()
    {
        if (Manager.GameOver) gameObject.SetActive(false);
        
        Rect r = new Rect(Rekt.rect);

        var percentLeft = Manager.TimeLeft / Manager.CurrentDuration;
        
        var newSize = Mathf.Lerp(0, StartingWidth, percentLeft);

        if (ChangeColor)
        {
            var newColor = (byte)Mathf.Lerp(0, 255, percentLeft);

            Im.color = new Color32( Manager.IsDay ? (byte)255 : newColor, !Manager.IsDay ? (byte)255 : newColor, !Manager.IsDay ? (byte)255 : newColor ,255);    
        }
        
        
        Rekt.sizeDelta = new Vector2(newSize, Rekt.sizeDelta.y);
    }
}
