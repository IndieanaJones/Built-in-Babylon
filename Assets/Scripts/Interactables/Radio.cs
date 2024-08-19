using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Radio : Interactable
{
    public bool IsOn = true;
    public AudioSource AudioSourceComp;

    public void Awake()
    {
        InteractString = "Toggle wind effects being played on the radio - (E)";
    }

    public override void OnInteract(Interactor interactor)
    {
        base.OnInteract(interactor);
        IsOn = !IsOn;
        AudioSource.PlayClipAtPoint((AudioClip)Resources.Load("Sounds/Tape End"), transform.position);
        if (!IsOn)
            AudioSourceComp.Stop();
        else
            AudioSourceComp.Play();
    }
}
