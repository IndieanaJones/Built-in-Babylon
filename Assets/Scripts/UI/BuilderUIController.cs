using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuilderUIController : MonoBehaviour
{
    public RockCollector RockCollectorComponent;
    public Text RockCollectorCounter;

    public void Update()
    {
        RockCollectorCounter.text = RockCollectorComponent.RocksCollected.ToString();
        RockCollectorCounter.color = (RockCollectorComponent.RocksCollected == RockCollectorComponent.MaximumRockCapacity ? Color.red : Color.white);
    }
}
