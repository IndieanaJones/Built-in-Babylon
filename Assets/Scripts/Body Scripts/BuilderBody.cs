using KinematicCharacterController;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuilderBody : BaseBody
{
    [Header("Component Refs")]
    public BuilderAnimator BuilderAnimatorComp;
    public RockCollector RockCollectorComp;
    public Interactor InteractorComp;

    [Header("Movement")]
    public float moveSpeed = 18.5f;
    public float maxAcceleration = 1.8f;
    public float backPedalModifier = 0.8f;
    public float SpeedMultiplier = 1f;

    [Header("Jumping")]
    public float jumpForce = 18f;
    public float gravity = 1.3f;
    public float airControlMultiplier = 0.5f;
    private bool _requestedJump = false;

    [Header("Landing")]
    public bool wasInAirLastCheck = false;

    [Header("Attack")]
    public float AttackCooldownTimer = -1f;
    public float AttackCoolown = 1f;
    public int MiningPower = 5;
    public int AttackPower = 2;
    public float PickaxeRange = 4;

    [Header("Chiseling")]
    public bool isChiseling = false;

    public void Update()
    {
        HandleCameraBase();
        CheckJumpInput();
        HandleAttack();
        HandleInteract();
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

    public void HandleAttack()
    {
        if (AttackCooldownTimer > Time.time || characterMaster == null || !characterMaster.primaryFireHeld || isChiseling)
            return;
        AttackCooldownTimer = Time.time + AttackCoolown;
        BuilderAnimatorComp.HandleAttack("pickaxe");
    }

    public void DoPickaxeHitscan()
    {
        RaycastHit hit;
        if (Physics.Raycast(ourCamera.transform.position, Quaternion.Euler(cameraXRotation, cameraYRotation, 0) * Vector3.forward, out hit, PickaxeRange))
        {
            if(hit.collider.GetComponent<Rock>())
            {
                Rock hitRock = hit.collider.GetComponent<Rock>();
                int rocksToHarvest = Mathf.Min(MiningPower + ProgressionManager.MiningPowerAdditive, RockCollectorComp.MaximumRockCapacity - RockCollectorComp.RocksCollected);
                hitRock.OnHarvest(RockCollectorComp, rocksToHarvest);
                DoDecalSpawn(hit);
                if (rocksToHarvest <= 0)
                    PlaySound("Pickaxe Contact Non-Rock", 0.1f);
                else
                    PlaySound("Pickaxe Contact Rock", 0.1f);
                hitRock.AdjustYLevel();
            }
            else if(hit.collider != null)
            {
                PlaySound("Pickaxe Contact Non-Rock", 0.1f);
                DoDecalSpawn(hit);
            }
        }
    }

    public void DoDecalSpawn(RaycastHit raycastHit)
    {
        GameObject bulletDecalPrefab = (GameObject)Resources.Load("Decals/Pickaxe Impact Decal");
        GameObject bulletDecal = Instantiate(bulletDecalPrefab, raycastHit.point, Quaternion.LookRotation(raycastHit.normal));
        bulletDecal.transform.position += bulletDecal.transform.forward * 0.001f;
        bulletDecal.transform.parent = raycastHit.transform;
    }

    public void HandleInteract()
    {
        if (characterMaster == null)
            return;
        isChiseling = false;
        if (characterMaster.interactPressed && InteractorComp.NearestInteractable != null)
            InteractorComp.NearestInteractable.OnInteract(InteractorComp);
        else if (characterMaster.interactHeld && InteractorComp.NearestInteractable != null && InteractorComp.NearestInteractable.CanInteractHold)
        {
            InteractorComp.NearestInteractable.OnInteractHeld(InteractorComp);
            if(InteractorComp.NearestInteractable.InteractHoldUseChiselAnimation)
                isChiseling = true;
        }
    }

    public override void Die()
    {
        base.Die();
        Destroy(gameObject);
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
        if (isChiseling)
            input = Vector3.zero;
        input.Normalize();

        Vector3 targetVelocity = input * moveSpeed * (verticalInput < 0 ? backPedalModifier : 1) * SpeedMultiplier;
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

    public override void OnGroundHit(Collider hitCollider, Vector3 hitNormal, Vector3 hitPoint, ref HitStabilityReport hitStabilityReport)
    {
        base.OnGroundHit(hitCollider, hitNormal, hitPoint, ref hitStabilityReport);
        if (hitCollider.GetComponent<MeshRenderer>())
        {
            if (hitCollider.GetComponent<MeshRenderer>().sharedMaterial.name == "Road")
                SpeedMultiplier = 2f;
            else if (hitCollider.GetComponent<MeshRenderer>().sharedMaterial.name.Contains("Carpet"))
                SpeedMultiplier = 1.5f;
            else
                SpeedMultiplier = 1;
        }
        else
            SpeedMultiplier = 1;
    }
}
