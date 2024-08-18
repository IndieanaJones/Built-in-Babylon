using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildSign : Interactable
{
    public int RocksRequired = 100;
    public int RocksGathered = 0;
    public string ConnectedBuildProject = "";
    public float BuildPercent = 0;

    public GameObject Rock1;
    public GameObject Rock2;
    public GameObject Rock3;

    public GameObject[] ObjectsToDisable;
    public GameObject[] ObjectsToEnable;

    public override void Update()
    {
        base.Update();
        UpdateInteractText();
    }

    public void UpdateInteractText()
    {
        if (RocksRequired > RocksGathered)
            InteractString = "(E) Construct " + ConnectedBuildProject + " - " + RocksRequired + " Rocks (" + RocksGathered + "/" + RocksRequired + ")";
        else if (BuildPercent <= 0)
            InteractString = "(E) Construct " + ConnectedBuildProject + " - " + "(Hold E)";
        else
            InteractString = "(E) Construct " + ConnectedBuildProject + " - " + "(" + Mathf.Round(BuildPercent * 100) + "% completed)";
    }

    public override void OnInteract(Interactor interactor)
    {
        if (RocksRequired > RocksGathered)
        {
            base.OnInteract(interactor);
            int rocksToRemove = Mathf.Min(RocksRequired - RocksGathered, interactor.GetComponent<RockCollector>().RocksCollected);
            RocksGathered += rocksToRemove;
            interactor.GetComponent<RockCollector>().RocksCollected -= rocksToRemove;
            if(rocksToRemove > 0)
                AudioSource.PlayClipAtPoint((AudioClip)Resources.Load("Sounds/Rock Drop"), transform.position);
            else
                AudioSource.PlayClipAtPoint((AudioClip)Resources.Load("Sounds/No Rocks"), transform.position);

            if (RocksGathered > 1)
                Rock1.SetActive(true);
            if (RocksGathered > RocksRequired / 2)
                Rock2.SetActive(true);
            if (RocksGathered == RocksRequired)
            {
                Rock3.SetActive(true);
                CanInteractHold = true;
                InteractHoldUseChiselAnimation = true;
            }
        }
    }

    public override void OnInteractHeld(Interactor interactor)
    {
        if (RocksRequired != RocksGathered)
            return;
        BuildPercent = Mathf.Min(1, BuildPercent + (0.05f * Time.deltaTime));
        if (BuildPercent >= 1)
            DoCompletionEffects();
    }

    public void DoCompletionEffects()
    {
        AudioSource.PlayClipAtPoint(((AudioClip)Resources.Load("Sounds/Building Complete")), transform.position);
        foreach (GameObject toBeEnabled in ObjectsToEnable)
            toBeEnabled.SetActive(true);
        foreach (GameObject toBeDisabled in ObjectsToDisable)
            toBeDisabled.SetActive(false);
        Destroy(gameObject);
    }
}
