using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SunAltar : Interactable
{
    public bool HasPraised = false;
    public SunScript SunScriptComp;

    public void Awake()
    {
        InteractString = "Praise the sun (E)";
    }

    public override void OnInteract(Interactor interactor)
    {
        base.OnInteract(interactor);
        if (!HasPraised)
        {
            HasPraised = true;
            SunScriptComp.EndGame();
        }
    }
}
