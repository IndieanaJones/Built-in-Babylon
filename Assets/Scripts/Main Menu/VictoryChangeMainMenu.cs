using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VictoryChangeMainMenu : MonoBehaviour
{
    public GameObject SunObject;
    public GameObject AngelObject;

    public void Start()
    {
        if(PlayerPrefs.GetInt("wongame") == 1)
        {
            SunObject.SetActive(true);
            AngelObject.SetActive(false);
        }
    }
}
