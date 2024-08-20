using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DeathScreen : MonoBehaviour
{
    public static DeathScreen Instance;
    public GameObject DeathScreenCanvas;
    public Text TimeAliveText;
    public bool DeathScreenSetup = false;

    public void Awake()
    {
        Instance = this;
    }

    public void Update()
    {
        if(!DeathScreenSetup && PlayerSpawner.ThePlayerRef == null)
        {
            SetUpDeathScreen();
            DeathScreenSetup = true;
        }
    }

    public void SetUpDeathScreen()
    {
        DirectCameraSound.Instance.PlaySound("Jingle Lost");
        DeathScreenCanvas.SetActive(true);
        if(AngelTalker.Instance.TimeGameStarted != -1)
            TimeAliveText.text = "Time Survived: " + TimeSpan.FromSeconds(Time.time - AngelTalker.Instance.TimeGameStarted);
        else
            TimeAliveText.text = "You somehow died before the game started...";
    }

    public void ContinuePressed()
    {
        DirectCameraSound.Instance.PlaySound("Pickaxe Contact Non-Rock");
        SceneManager.LoadScene("TheDesert");
    }

    public void GoToMainMenuPressed()
    {
        DirectCameraSound.Instance.PlaySound("Pickaxe Contact Non-Rock");
        Time.timeScale = 1;
        SceneManager.LoadScene("MainMenu");
    }
}
