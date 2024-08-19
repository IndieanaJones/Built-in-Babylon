using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockCollector : MonoBehaviour
{
    public int RocksCollected = 0;
    public int MaximumRockCapacity = 100;
    public bool HasInfiniteRocks = false;

    public void FixedUpdate()
    {
        if (HasInfiniteRocks)
            RocksCollected = MaximumRockCapacity;
    }
}
