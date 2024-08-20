using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flood : MonoBehaviour
{
    public static Flood Instance;

    public GameObject WaterObject;
    public GameObject RainfallEffect;
    public GameObject CanvasUnderwaterEffect;
    public AudioSource RainSoundLoop;

    public float WaterLevel = -10;
    public bool FloodActive = false;
    public bool DebugToggleFlood = false;
    public float TimeWhenFloodStarted = -1;
    public int Stage = -1;

    public float LastTimePlayerHurt = -1;

    public void Start()
    {
        Instance = this;
    }

    public void BeginFlood()
    {
        TimeWhenFloodStarted = Time.time;
        FloodActive = true;
        Stage = 0;
        RainSoundLoop.Play();
    }

    public void EndFlood()
    {
        RainfallEffect.transform.position = new Vector3(0, -10f, 0);
        FloodActive = false;
    }

    public void Update()
    {
        if (PlayerSpawner.ThePlayerRef == null)
        {
            RainSoundLoop.Stop();
            return;
        }
        if (PlayerSpawner.ThePlayerRef.transform.position.y + 2.1f < WaterLevel)
        {
            CanvasUnderwaterEffect.SetActive(true);
            if (LastTimePlayerHurt + 1.25f < Time.time)
            {
                LastTimePlayerHurt = Time.time;
                PlayerSpawner.ThePlayerRef.GetComponent<Health>().TakeDamage(1, true);
            }
        }
        else
            CanvasUnderwaterEffect.SetActive(false);
        if (DebugToggleFlood)
        {
            DebugToggleFlood = false;
            BeginFlood();
        }
        if (FloodActive)
            ProcessFlood();
    }

    public void ProcessFlood()
    {
        float Age = Time.time - TimeWhenFloodStarted;
        WaterLevel = WaterObject.transform.position.y + 10;
        RainfallEffect.transform.position = new Vector3(PlayerSpawner.ThePlayerRef.transform.position.x, PlayerSpawner.ThePlayerRef.transform.position.y + 20, PlayerSpawner.ThePlayerRef.transform.position.z);
        if(Stage == 0)
        {
            RainfallEffect.SetActive(true);
            WaterObject.transform.position = Vector3.Lerp(new Vector3(0, -10, 0), new Vector3(0, 5, 0), Age / (30));
            if (Age >= 30)
                Stage++;
        }
        if(Stage == 1)
        {
            if (Age >= 40)
            {
                RainfallEffect.SetActive(false);
                RainSoundLoop.Stop();
                Stage++;
            }
        }
        if(Stage == 2)
        {
            WaterObject.transform.position = Vector3.Lerp(new Vector3(0, 5, 0), new Vector3(0, -10, 0), (Age - 40) / 10);
            if (Age >= 50)
                Stage++;
        }
        if(Stage == 3)
        {
            EndFlood();
        }
    }
}
