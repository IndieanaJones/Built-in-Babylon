using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCharacterMaster : CharacterMaster
{
    float _horizontalCameraSpeed = 1f;
    float _verticalCameraSpeed = 1f;

    void Update()
    {
        horizontalCameraInput = Input.GetAxis("Mouse X") * _horizontalCameraSpeed;
        verticalCameraInput = Input.GetAxis("Mouse Y") * _verticalCameraSpeed;

        jumpPressed = Input.GetKeyDown(KeyCode.Space);

        primaryFireHeld = Input.GetMouseButton(0);
        shiftHeld = Input.GetKey(KeyCode.LeftShift);

        movementXAxisInput = (Input.GetKey(KeyCode.D) ? 1 : 0) + (Input.GetKey(KeyCode.A) ? -1 : 0);
        movementYAxisInput = (Input.GetKey(KeyCode.W) ? 1 : 0) + (Input.GetKey(KeyCode.S) ? -1 : 0);

        interactPressed = Input.GetKeyDown(KeyCode.E);
        interactHeld = Input.GetKey(KeyCode.E);
    }
}
