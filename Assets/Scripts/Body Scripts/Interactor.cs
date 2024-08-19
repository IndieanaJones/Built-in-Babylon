using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactor : MonoBehaviour
{
    public BaseBody BaseBodyComp;
    public Interactable NearestInteractable;

    public void FixedUpdate()
    {
        CalculateNearestInteractable();
    }

    public void CalculateNearestInteractable()
    {
        RaycastHit hit;
        if (Physics.Raycast(BaseBodyComp.ourCamera.transform.position, Quaternion.Euler(BaseBodyComp.cameraXRotation, BaseBodyComp.cameraYRotation, 0) * Vector3.forward, out hit, 6))
        {
            if (hit.collider != null && hit.collider.GetComponent<Interactable>())
            {
                NearestInteractable = hit.collider.GetComponent<Interactable>();
                return;
            }
        }
        NearestInteractable = null;
    }
}
