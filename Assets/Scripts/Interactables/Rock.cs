using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rock : MonoBehaviour
{
    public int RocksRemaining = 100;
    public float InitialYPosition = 0;
    public bool DoRandomRotation = true;
    public bool BreakingTriggersDialogue = false;
    public int DialogueToTrigger = -1;

    public void Awake()
    {
        InitialYPosition = transform.position.y;
        if(DoRandomRotation)
            transform.rotation = Quaternion.Euler(0, UnityEngine.Random.Range(0, 360), 0);
    }

    public void OnHarvest(RockCollector collector, int miningStrength)
    {
        int rocksHarvested = Mathf.Min(RocksRemaining, miningStrength);
        RocksRemaining -= rocksHarvested;
        collector.RocksCollected += rocksHarvested;
    }

    public void AdjustYLevel()
    {
        if (RocksRemaining == 0)
        {
            AudioSource.PlayClipAtPoint(((AudioClip)Resources.Load("Sounds/Rock Destroy")), transform.position);
            if (BreakingTriggersDialogue)
                AngelTalker.Instance.DoAngelLineTrigger(DialogueToTrigger);
            Destroy(gameObject);
        }
        else if (RocksRemaining <= 20)
            transform.position = new Vector3(transform.position.x, InitialYPosition - 11.5f, transform.position.z);
        else if (RocksRemaining <= 50)
            transform.position = new Vector3(transform.position.x, InitialYPosition - 7, transform.position.z);
        else if (RocksRemaining <= 80)
            transform.position = new Vector3(transform.position.x, InitialYPosition - 3, transform.position.z);
        else
            transform.position = new Vector3(transform.position.x, InitialYPosition, transform.position.z);
    }
}
