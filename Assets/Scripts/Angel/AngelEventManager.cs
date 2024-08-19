using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AngelEventManager : MonoBehaviour
{
    public static AngelEventManager Instance;
    public bool PauseEventTimer = true;
    public float TimeWhenLastEventOccurred = 0;
    public float TimeForNextEvent = 15f;
    public List<string> AvailableEvents = new List<string>();

    public int MaxMummies = 10;

    public void Start()
    {
        Instance = this;
    }

    public void FixedUpdate()
    {
        if(AvailableEvents.Count > 0 && TimeForNextEvent <= Time.time)
        {
            DoEvent();
        }
    }

    public void AddEvent(string eventName)
    {
        AvailableEvents.Add(eventName);
    }

    public void DoEvent()
    {
        string randomEvent = AvailableEvents[Random.Range(0, AvailableEvents.Count)];
        switch(randomEvent)
        {
            case "lightning":
                StartCoroutine(DirectLightningStrike());
                break;
            default:
                break;
        }
    }

    public IEnumerator DirectLightningStrike()
    {
        TimeForNextEvent = Time.time + 10f + UnityEngine.Random.Range(-4, 5);
        yield return new WaitForSeconds(2);
        GameObject lightningCloud = (GameObject)Resources.Load("Prefabs/Lightning Bolt");
        Vector3 playerPos = PlayerSpawner.ThePlayerRef.transform.position;
        Instantiate(lightningCloud, new Vector3(playerPos.x, playerPos.y - 2.2f, playerPos.z), Quaternion.identity);
    }
}
