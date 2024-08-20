using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealingPool : Interactable
{
    public bool IsAvailable = true;
    public GameObject LiquidBit;

    public void Awake()
    {
        InteractString = "Drink Healing 'Liquid' (Refills over time) (E)";
    }

    public override void OnInteract(Interactor interactor)
    {
        base.OnInteract(interactor);
        if (IsAvailable)
            DrinkLiquid();
        else
            FailDrinkLiquid();
    }

    public void DrinkLiquid()
    {
        DirectCameraSound.Instance.PlaySound("Sip");
        PlayerSpawner.ThePlayerRef.GetComponent<Health>().Heal(3);
        IsAvailable = false;
        LiquidBit.SetActive(false);
        InteractString = "Bowl is empty currently. Come back later!";
        StartCoroutine(RefillLiquid());
    }

    public void FailDrinkLiquid()
    {
        DirectCameraSound.Instance.PlaySound("Pickaxe Contact Non-Rock");
    }

    public IEnumerator RefillLiquid()
    {
        yield return new WaitForSeconds(60);
        IsAvailable = true;
        LiquidBit.SetActive(true);
        InteractString = "Drink Healing 'Liquid' (Refills over time) (E)";
    }
}
