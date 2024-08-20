using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetUpGameStart : MonoBehaviour
{
    public GameObject[] ThingsToDisable;
    public GameObject[] ThingsToEnable;

    public BuildSign SignToSkipTutorial;

    public void Start()
    {
        ProgressionManager.TutorialSkipped = PlayerPrefs.GetInt("skiptutorial") == 1;
        foreach(GameObject leObject in ThingsToDisable)
        {
            leObject.SetActive(false);
        }
        foreach(GameObject leObject in ThingsToEnable)
        {
            leObject.SetActive(true);
        }
        if (ProgressionManager.TutorialSkipped)
            SignToSkipTutorial.DoCompletionEffects();
    }
}
