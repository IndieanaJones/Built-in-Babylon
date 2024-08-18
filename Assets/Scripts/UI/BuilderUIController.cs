using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuilderUIController : MonoBehaviour
{
    public RockCollector RockCollectorComponent;
    public Interactor InteractorComponent;
    public Text RockCollectorCounter;
    public Text InteractText;

    public void Update()
    {
        RockCollectorCounter.text = RockCollectorComponent.RocksCollected.ToString();
        RockCollectorCounter.color = RockCollectorComponent.RocksCollected == RockCollectorComponent.MaximumRockCapacity ? Color.red : Color.white;
        InteractText.text = InteractorComponent.NearestInteractable != null ? InteractorComponent.NearestInteractable.InteractString : "";
    }
}
