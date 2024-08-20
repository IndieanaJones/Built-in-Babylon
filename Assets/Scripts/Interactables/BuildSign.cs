using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildSign : Interactable
{
    public int RocksRequired = 100;
    public int RocksGathered = 0;
    public string ConnectedBuildProject = "";
    public float BuildPercent = 0;
    public float BuildSpeedPerSecond = 0.25f;

    public bool AvailableAtStart = false;

    public GameObject Rock1;
    public GameObject Rock2;
    public GameObject Rock3;

    public bool TriggersDialogueOnRockDeposit = false;
    public int RockDepositLine = -1;

    public bool TriggersDialogueOnBuildStart = false;
    public int BuildStartLine = -1;

    public bool TriggersDialogueOnBuildComplete = false;
    public int BuildCompleteLine = -1;

    public GameObject[] ObjectsToDisable;
    public GameObject[] ObjectsToEnable;

    public int MinePowerAdditiveChange = 0;
    public float JumpHeightChange = 0;

    public virtual void Awake()
    {
        if (!AvailableAtStart)
        {
            foreach (GameObject toBeEnabled in ObjectsToEnable)
                if(toBeEnabled.GetComponent<BuildSign>() == null)
                    toBeEnabled.SetActive(false);
            gameObject.SetActive(false);
        }
        else
        {
            foreach (GameObject toBeEnabled in ObjectsToEnable)
                toBeEnabled.SetActive(false);
        }
    }

    public override void Update()
    {
        base.Update();
        UpdateInteractText();
    }

    public virtual void UpdateInteractText()
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
                StartCoroutine(DelayToInteractHold());
                if (TriggersDialogueOnRockDeposit)
                    AngelTalker.Instance.DoAngelLineTrigger(RockDepositLine);
            }
        }
    }

    public IEnumerator DelayToInteractHold()
    {
        yield return new WaitForSeconds(0.5f);
        CanInteractHold = true;
        InteractHoldUseChiselAnimation = true;
    }

    public override void OnInteractHeld(Interactor interactor)
    {
        if (interactor.Instabuilder)
        {
            if (TriggersDialogueOnBuildComplete)
                AngelTalker.Instance.DoAngelLineTrigger(BuildCompleteLine);
            DoCompletionEffects();
        }
        if (RocksRequired != RocksGathered)
            return;
        if(BuildPercent == 0 && TriggersDialogueOnBuildStart)
            AngelTalker.Instance.DoAngelLineTrigger(BuildStartLine);
        BuildPercent = Mathf.Min(1, BuildPercent + (BuildSpeedPerSecond * Time.deltaTime));
        if (BuildPercent >= 1)
        {
            if(TriggersDialogueOnBuildComplete)
                AngelTalker.Instance.DoAngelLineTrigger(BuildCompleteLine);
            DoCompletionEffects();
        }
    }

    public virtual void DoCompletionEffects()
    {
        DirectCameraSound.Instance.PlaySound("Building Complete");
        foreach (GameObject toBeEnabled in ObjectsToEnable)
            toBeEnabled.SetActive(true);
        foreach (GameObject toBeDisabled in ObjectsToDisable)
            toBeDisabled.SetActive(false);
        ProgressionManager.MiningPowerAdditive += MinePowerAdditiveChange;
        if(PlayerSpawner.ThePlayerRef != null)
            PlayerSpawner.ThePlayerRef.GetComponent<BuilderBody>().jumpForce += JumpHeightChange;
        Destroy(gameObject);
    }
}
