using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    public List<Interactor> NearbyInteractors;
    public string InteractString = "Dummy";

    public bool CanInteractHold = false;
    public bool InteractHoldUseChiselAnimation = false;

    public virtual void Update()
    {

    }

    public virtual void OnInteract(Interactor interactor)
    {

    }

    public virtual void OnInteractHeld(Interactor interactor)
    {

    }
}
