using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPSPerspective : MonoBehaviour
{
    public GameObject thirdPersonModel;
    public GameObject firstPersonModel;
    public GameObject cameraHolder;
    public GameObject canvas;

    public bool isInFPS = false;

    public bool debugToggleFPS = false;

    public void Update()
    {
        if(debugToggleFPS)
        {
            debugToggleFPS = false;
            EnableFPS(!isInFPS);
        }
    }

    public void EnableFPS(bool toEnable)
    {
        if (thirdPersonModel != null)
        {
            Renderer[] renderers = thirdPersonModel.GetComponentsInChildren<Renderer>();
            foreach (Renderer renderer in renderers)
                renderer.shadowCastingMode = toEnable ? UnityEngine.Rendering.ShadowCastingMode.ShadowsOnly : UnityEngine.Rendering.ShadowCastingMode.TwoSided;
        }
        if (firstPersonModel != null)
            firstPersonModel.SetActive(toEnable);
        if (toEnable)
        {
            Camera.main.transform.parent = cameraHolder.transform;
            Camera.main.transform.localPosition = Vector3.zero;
            Camera.main.transform.localRotation = Quaternion.identity;
            Cursor.lockState = CursorLockMode.Locked;
        }
        if(canvas != null)
            canvas.SetActive(toEnable);
        isInFPS = toEnable;
    }
}
