using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProgressionManager : MonoBehaviour
{
    public int Progression = 0;
    public float MiningSpeedMult = 1;
    public float MiningPowerAdditiveMult = 1;
    public float MiningPowerMult = 1;
    public float MoveSpeedMult = 1;
    public static bool TutorialSkipped = false;
    public static bool IntroFinished = false;
}
