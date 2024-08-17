using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuilderBody : BaseBody
{
    [Header("Movement")]
    public float moveSpeed = 18.5f;
    public float maxAcceleration = 1.8f;
    public float backPedalModifier = 0.8f;

    [Header("Jumping")]
    public float jumpForce = 18f;
    public float gravity = 1.3f;
    public float airControlMultiplier = 0.5f;
    private bool _requestedJump = false;

    [Header("Landing")]
    public bool wasInAirLastCheck = false;

    public void Update()
    {
        HandleCameraBase();
        CheckJumpInput();
    }

    public void CheckJumpInput()
    {
        if (characterMaster == null)
            return;
        if (characterMaster != null && characterMaster.jumpPressed && kinematicCC.GroundingStatus.IsStableOnGround)
        {
            _requestedJump = true;
        }
    }

    public override void UpdateCharacterRotation(ref Quaternion currentRotation, float deltaTime)
    {
        if (characterMaster == null)
            return;
        currentRotation = Quaternion.Euler(new Vector3(0, cameraYRotation, 0));
    }

    public override void UpdateCharacterVelocity(ref Vector3 currentVelocity, float deltaTime)
    {
        float currentYAcceleration = currentVelocity.y;

        float horizontalInput = (characterMaster != null ? characterMaster.movementXAxisInput : 0);
        float verticalInput = (characterMaster != null ? characterMaster.movementYAxisInput : 0);

        Vector3 input = transform.right * horizontalInput + transform.forward * verticalInput;
        input.Normalize();

        Vector3 targetVelocity = input * moveSpeed * (verticalInput < 0 ? backPedalModifier : 1);
        Vector3 acceleration = targetVelocity - currentVelocity;
        acceleration = Vector3.ClampMagnitude(acceleration, maxAcceleration * (!kinematicCC.GroundingStatus.IsStableOnGround ? airControlMultiplier : 1));
        currentVelocity += acceleration;

        if (_requestedJump)
        {
            _requestedJump = false;
            if (kinematicCC.GroundingStatus.IsStableOnGround)
            {
                kinematicCC.ForceUnground();
                currentVelocity.y = jumpForce;
            }
        }
        else if (kinematicCC.GroundingStatus.IsStableOnGround)
            currentVelocity.y = 0;
        else
            currentVelocity.y = currentYAcceleration - gravity;
    }
}
