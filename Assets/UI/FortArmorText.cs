using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FortArmorText : MonoBehaviour
{
    public FortScript Fort;

    public TextMeshProUGUI Text;

    public GameManager Manager;
    
    private void Start()
    {
        Manager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Manager.GameOver)
        {
            this.gameObject.SetActive(false);
        }
        else
        {
            Text.text = $"Fort Armor: {Fort.Armor}";

        }
    }
}
