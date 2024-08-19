using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuilderUIController : MonoBehaviour
{
    public RockCollector RockCollectorComponent;
    public Health HealthComponent;
    public Interactor InteractorComponent;
    public Text RockCollectorCounter;
    public Text InteractText;

    public Image HealthImage;
    public Sprite Coconut10;
    public Sprite Coconut9;
    public Sprite Coconut8;
    public Sprite Coconut7;
    public Sprite Coconut6;
    public Sprite Coconut5;
    public Sprite Coconut4;
    public Sprite Coconut3;
    public Sprite Coconut2;
    public Sprite Coconut1;

    public Text HealthValue;

    public void Update()
    {
        RockCollectorCounter.text = RockCollectorComponent.RocksCollected.ToString();
        RockCollectorCounter.color = RockCollectorComponent.RocksCollected == RockCollectorComponent.MaximumRockCapacity ? Color.red : Color.white;
        InteractText.text = InteractorComponent.NearestInteractable != null ? InteractorComponent.NearestInteractable.InteractString : "";

        HealthValue.text = HealthComponent.CurrentHealth.ToString();
        switch(HealthComponent.CurrentHealth)
        {
            case 10:
                HealthImage.sprite = Coconut10;
                break;
            case 9:
                HealthImage.sprite = Coconut9;
                break;
            case 8:
                HealthImage.sprite = Coconut8;
                break;
            case 7:
                HealthImage.sprite = Coconut7;
                break;
            case 6:
                HealthImage.sprite = Coconut6;
                break;
            case 5:
                HealthImage.sprite = Coconut5;
                break;
            case 4:
                HealthImage.sprite = Coconut4;
                break;
            case 3:
                HealthImage.sprite = Coconut3;
                break;
            case 2:
                HealthImage.sprite = Coconut2;
                break;
            case 1:
                HealthImage.sprite = Coconut1;
                break;
        }
    }
}
