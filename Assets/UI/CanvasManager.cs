using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasManager : MonoBehaviour
{
    public GameObject GameOverScreen;

    public GameObject GameplayScreen;

    public GameManager Manager;

    private void Update()
    {
        if (Manager.GameOver)
        {
            GameplayScreen.SetActive(false);
            GameOverScreen.SetActive(true);
        }
    }
}
