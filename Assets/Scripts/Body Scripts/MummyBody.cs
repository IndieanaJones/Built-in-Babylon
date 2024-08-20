using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MummyBody : BaseBody
{
    [Header("Component Refs")]
    public MummyAnimator MummyAnimatorComp;

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
    public int AttackPower = 2;
    public float AttackRange = 4;

    [Header("Mummy")]
    public bool IsEmerging = false;
    public List<GameObject> BodyParts;
    public ParticleSystem MummyBlood;

    public void Update()
    {
        HandleCameraBase();
        CheckJumpInput();
        HandleAttack();
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
        if (AttackCooldownTimer > Time.time || characterMaster == null || !characterMaster.primaryFireHeld || IsEmerging)
            return;
        AttackCooldownTimer = Time.time + AttackCoolown;
        MummyAnimatorComp.HandleAttack("swipe");
    }

    public void DoMeleeHitscan()
    {
        RaycastHit hit;
        if (Physics.Raycast(ourCamera.transform.position, Quaternion.Euler(cameraXRotation, cameraYRotation, 0) * Vector3.forward, out hit, AttackRange))
        {
            if (hit.collider.gameObject == gameObject)
                return;
            else if (hit.collider.GetComponent<Health>())
            {
                Health EnemyHealth = hit.collider.GetComponent<Health>();
                EnemyHealth.TakeDamage(AttackPower);
            }
        }
    }

    public override void Die()
    {
        base.Die();
        MummyBlood.Play();
        MummyBlood.transform.SetParent(null);
        foreach (GameObject part in BodyParts)
        {
            part.transform.SetParent(null);
            BoxCollider boxCollider = part.gameObject.AddComponent(typeof(BoxCollider)) as BoxCollider;
            Rigidbody rigidbody = part.gameObject.AddComponent(typeof(Rigidbody)) as Rigidbody;
            Vector3 velocity = new Vector3(UnityEngine.Random.Range(-200.0f, 200.0f), UnityEngine.Random.Range(0.0f, 500.0f), UnityEngine.Random.Range(-200.0f, 200.0f));
            rigidbody.AddForce(velocity);
            Destroy(part, 10);
        }
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
        if (IsEmerging)
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
}
