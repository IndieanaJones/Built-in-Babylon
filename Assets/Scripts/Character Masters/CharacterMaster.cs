using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMaster : MonoBehaviour
{
    public float horizontalCameraInput;
    public float verticalCameraInput;

    public bool jumpPressed;

    public bool primaryFireHeld;
    public bool shiftHeld;

    public float movementXAxisInput;
    public float movementYAxisInput;

    public virtual void PostMoveUpdate()
    {

    }
}
