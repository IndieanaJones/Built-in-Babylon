using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class WinScreen : MonoBehaviour
{
    public static WinScreen Instance;
    public GameObject WinScreenCanvas;
    public Text TimeTotalText;
    public bool WinScreenUp = false;

    public void Awake()
    {
        Instance = this;
    }

    public void SetUpWinScreen()
    {
        WinScreenCanvas.SetActive(true);
        WinScreenUp = true;
        TimeTotalText.text = "Total Time: " + TimeSpan.FromSeconds(SunScript.EndTime);
        if(PlayerPrefs.GetFloat("besttime") == 0 || PlayerPrefs.GetFloat("besttime") > SunScript.EndTime)
            PlayerPrefs.SetFloat("besttime", SunScript.EndTime);
        PlayerPrefs.SetInt("wongame", 1);
    }

    public void GoToMainMenuPressed()
    {
        DirectCameraSound.Instance.PlaySound("Pickaxe Contact Non-Rock");
        Time.timeScale = 1;
        SceneManager.LoadScene("MainMenu");
    }
}
