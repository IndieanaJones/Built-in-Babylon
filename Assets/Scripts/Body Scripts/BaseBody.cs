using KinematicCharacterController;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseBody : MonoBehaviour, ICharacterController
{
    [Header("Component Refs Base")]
    public AudioSource OurAudioSource;
    public KinematicCharacterMotor kinematicCC;
    public CharacterMaster characterMaster;
    public FPSPerspective fpsPerspective;

    [Header("Camera Properties Base")]
    public GameObject ourCamera;
    public bool DontHandleCamera = false;
    public float cameraYRotation = 0;
    public float cameraXRotation = 0;
    public const float MAX_LOOK_ROTATION = 89.99f;
    public const float MIN_LOOK_ROTATION = -89.99f;

    public virtual void Awake()
    {
        kinematicCC.CharacterController = this;
    }

    public void HandleCameraBase()
    {
        if (characterMaster == null || DontHandleCamera)
            return;
        float mouseX = characterMaster.horizontalCameraInput;
        cameraYRotation += mouseX;
        cameraYRotation = cameraYRotation % 360;
        float mouseY = characterMaster.verticalCameraInput;
        cameraXRotation -= mouseY;
        cameraXRotation = Mathf.Clamp(cameraXRotation, MIN_LOOK_ROTATION, MAX_LOOK_ROTATION);
        ourCamera.transform.localRotation = Quaternion.Euler(new Vector3(cameraXRotation, 0, 0.0f));
    }

    public void PlaySound(AnimationEvent anim)
    {
        OurAudioSource.pitch = 1 + Random.Range(-anim.floatParameter, anim.floatParameter);
        OurAudioSource.PlayOneShot((AudioClip)Resources.Load("Sounds/" + anim.stringParameter));
    }

    public void PlaySound(string soundName, float pitchShift)
    {
        OurAudioSource.pitch = 1 + Random.Range(-pitchShift, pitchShift);
        OurAudioSource.PlayOneShot((AudioClip)Resources.Load("Sounds/" + soundName));
    }

    public virtual void Die()
    {
        if (Camera.main.transform.IsChildOf(gameObject.transform))
            Camera.main.transform.SetParent(null);
    }

    //Override this in a body script
    public virtual void UpdateCharacterRotation(ref Quaternion currentRotation, float deltaTime)
    {

    }

    public void UpdateRotation(ref Quaternion currentRotation, float deltaTime)
    {
        UpdateCharacterRotation(ref currentRotation, deltaTime);
    }

    //Override this in a body script
    public virtual void UpdateCharacterVelocity(ref Vector3 currentVelocity, float deltaTime)
    {

    }

    public void UpdateVelocity(ref Vector3 currentVelocity, float deltaTime)
    {
        UpdateCharacterVelocity(ref currentVelocity, deltaTime);
    }

    public void BeforeCharacterUpdate(float deltaTime)
    {
        return;
    }

    public void PostGroundingUpdate(float deltaTime)
    {
        return;
    }

    public void AfterCharacterUpdate(float deltaTime)
    {
        if(characterMaster)
            characterMaster.PostMoveUpdate();
        return;
    }

    public bool IsColliderValidForCollisions(Collider coll)
    {
        return true;
    }

    public virtual void OnGroundHit(Collider hitCollider, Vector3 hitNormal, Vector3 hitPoint, ref HitStabilityReport hitStabilityReport)
    {
        return;
    }

    public void OnMovementHit(Collider hitCollider, Vector3 hitNormal, Vector3 hitPoint, ref HitStabilityReport hitStabilityReport)
    {
        return;
    }

    public void ProcessHitStabilityReport(Collider hitCollider, Vector3 hitNormal, Vector3 hitPoint, Vector3 atCharacterPosition, Quaternion atCharacterRotation, ref HitStabilityReport hitStabilityReport)
    {
        return;
    }

    public void OnDiscreteCollisionDetected(Collider hitCollider)
    {
        return;
    }
}
