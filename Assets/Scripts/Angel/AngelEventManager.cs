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
    public List<GameObject> MummyList;

    public void Start()
    {
        Instance = this;
    }

    public void FixedUpdate()
    {
        List<GameObject> mummiesToRemove = new List<GameObject>();
        foreach(GameObject mummy in MummyList)
        {
            if (mummy == null)
                mummiesToRemove.Add(mummy);
        }
        foreach(GameObject mummy in mummiesToRemove)
        {
            MummyList.Remove(mummy);
        }
        if(AvailableEvents.Count > 0 && TimeForNextEvent <= Time.time && !PauseEventTimer)
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
        if (PlayerSpawner.ThePlayerRef == null)
            return;
        string randomEvent = AvailableEvents[Random.Range(0, AvailableEvents.Count)];
        switch(randomEvent)
        {
            case "lightning":
                StartCoroutine(DirectLightningStrike());
                break;
            case "mummies":
                StartCoroutine(SummonMummies(5));
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

    public IEnumerator SummonMummies(int summonCount)
    {
        TimeForNextEvent = Time.time + 20f + UnityEngine.Random.Range(-10, 10);
        yield return new WaitForSeconds(2);
        int AmountToSummon = Mathf.Min(summonCount, MaxMummies - MummyList.Count);
        GameObject mummy = (GameObject)Resources.Load("Prefabs/Mummy");
        for (int i = 0; i < AmountToSummon; i++)
        {
            Vector3 playerPos = PlayerSpawner.ThePlayerRef.transform.position;
            Vector3 AttemptedPosition = new Vector3(playerPos.x + Random.Range(-100.0f, 100.0f), playerPos.y + 20f, playerPos.z + Random.Range(-100.0f, 100.0f));
            RaycastHit hit;
            for (int spawnAttempts = 0; spawnAttempts < 10; spawnAttempts++)
            {
                if (Physics.Raycast(AttemptedPosition, Vector3.down, out hit, 200))
                {
                    if (hit.transform != null && hit.transform.gameObject.layer == 6)
                    {
                        GameObject newMummy = Instantiate(mummy, new Vector3(hit.point.x, hit.point.y + 2.2f, hit.point.z), Quaternion.identity);
                        MummyList.Add(newMummy);
                    }
                    else
                    {
                        spawnAttempts++;
                    }
                }
                else
                    spawnAttempts++;
            }
        }
    }
}
