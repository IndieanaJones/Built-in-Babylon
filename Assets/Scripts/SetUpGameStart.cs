using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetUpGameStart : MonoBehaviour
{
    public GameObject[] ThingsToDisable;
    public GameObject[] ThingsToEnable;

    public void Awake()
    {
        foreach(GameObject leObject in ThingsToDisable)
        {
            leObject.SetActive(false);
        }
        foreach(GameObject leObject in ThingsToEnable)
        {
            leObject.SetActive(true);
        }
    }
}
