using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class FortArmorText : MonoBehaviour
{
    public TextMeshProUGUI GodText;

    public TextMeshProUGUI WaveText;

    public TextMeshProUGUI ZombiesText;
    
    public TextMeshProUGUI TimeText;
    
    public FortScript Fort;

    public TextMeshProUGUI ArmorText;
    
    public TextMeshProUGUI DamagedText;
    
    public TextMeshProUGUI CompletionText;

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
            ArmorText.text = $"Armor: {Fort.Armor}";
            CompletionText.text = $"Completion: {Fort.DoneParts.Count()}/{Fort.Parts.Length}";
            DamagedText.text = $"Damaged: {Fort.DamagedParts.Count()}";

            TimeText.text = Manager.IsDay ? "Gather while you can!" : "God will save you in.";

            WaveText.text = Manager.IsDay ? "" : $"Wave{Environment.NewLine}{Manager.Wave}";
            ZombiesText.text = Manager.IsDay ? "" : $"Zombies{Environment.NewLine}{Manager.Zombies.Count(x => !x.IsDead)}";
            
            GodText.text = Manager.IsDay ? "" : $"Lightning strike in{Environment.NewLine}{Manager.NextLightningStrike.ToString("0.00")}";
        }
    }
}
