using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AngelInteractable : BuildSign
{
    public Animator AngelAnimator;

    public override void Awake()
    {
        RocksRequired = 0;
        CanInteractHold = true;
        InteractHoldUseChiselAnimation = true;
        base.Awake();
    }

    public override void UpdateInteractText()
    {
         if (BuildPercent <= 0)
            InteractString = "(E) Get this thing out of the way of your sun - " + "(Hold E)";
        else
            InteractString = "(E) Get this thing out of the way of your sun - " + "(" + Mathf.Round(BuildPercent * 100) + "% completed)";
    }

    public override void DoCompletionEffects()
    {
        DirectCameraSound.Instance.PlaySound("Building Complete");
        DirectCameraSound.Instance.PlaySound("Angel Scream");
        AngelAnimator.Play("Move");
        AngelTalker.Instance.GameIsOver = true;
        Destroy(this);
    }
}
