using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public Image BlackScreenImage;
    public AudioSource AudioSourceComp;
    public bool ButtonPressed = false;

    public Text BestTimeText;

    public Text SkipTutorialText;

    public IEnumerator Start()
    {
        Time.timeScale = 1;
        float bestTime = PlayerPrefs.GetFloat("besttime");
        if (bestTime == 0)
            BestTimeText.text = "Best Time: Not Set!";
        else
        {
            string bestTimeTranslated = TimeSpan.FromSeconds(bestTime).ToString();
            BestTimeText.text = "Best Time: " + bestTimeTranslated;
        }
        SkipTutorialText.text = PlayerPrefs.GetInt("skiptutorial") == 1 ? "Skip Tutorial: Yes" : "Skip Tutorial: No";
        BlackScreenImage.gameObject.SetActive(true);
        yield return StartCoroutine(RemoveBlackscreen());
        BlackScreenImage.gameObject.SetActive(false);
    }

    public IEnumerator RemoveBlackscreen()
    {
        while (BlackScreenImage.color.a > 0)
        {
            Color tempColor = BlackScreenImage.color;
            tempColor.a -= 0.02f;
            BlackScreenImage.color = tempColor;
            yield return new WaitForSeconds(0.025f);
        }
    }

    public IEnumerator DoBlackscreen()
    {
        while (BlackScreenImage.color.a < 1)
        {
            Color tempColor = BlackScreenImage.color;
            tempColor.a += 0.02f;
            BlackScreenImage.color = tempColor;
            yield return new WaitForSeconds(0.025f);
        }
    }

    public void PlayPressed()
    {
        if (ButtonPressed)
            return;
        ButtonPressed = true;
        AudioSourceComp.Play();
        StartCoroutine(BeginPlay());
    }

    public IEnumerator BeginPlay()
    {
        BlackScreenImage.gameObject.SetActive(true);
        yield return StartCoroutine(DoBlackscreen());
        SceneManager.LoadScene("TheDesert");
    }

    public void QuitPressed()
    {
        if (ButtonPressed)
            return;
        ButtonPressed = true;
        AudioSourceComp.Play();
        StartCoroutine(BeginQuit());
    }

    public IEnumerator BeginQuit()
    {
        BlackScreenImage.gameObject.SetActive(true);
        yield return StartCoroutine(DoBlackscreen());
        Application.Quit();
    }

    public void SkipTutorialPressed()
    {
        if (PlayerPrefs.GetInt("skiptutorial") == 1)
            PlayerPrefs.SetInt("skiptutorial", 0);
        else
            PlayerPrefs.SetInt("skiptutorial", 1);
        SkipTutorialText.text = PlayerPrefs.GetInt("skiptutorial") == 1 ? "Skip Tutorial: Yes" : "Skip Tutorial: No";
    }
}
