using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AngelEnding : MonoBehaviour
{
    public void TriggerEnding()
    {
        AngelTalker.Instance.DoAngelLineTrigger(99);
    }
}
