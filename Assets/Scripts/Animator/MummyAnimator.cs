using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MummyAnimator : MonoBehaviour
{
    public MummyBody MummyBodyComp;
    public Animator ModelAnimator;

    public string ModelAnimation = "";
    public float AnimationSpeed = 1;

    public void Update()
    {
        UpdateModelAnimator();
    }

    public void UpdateModelAnimator()
    {
        string oldModelAnimation = ModelAnimation;
        AnimationSpeed = 1;
        float crossFadeTime = 0.1f;
        {
            Vector3 normalizedVelocity = Vector3.Normalize(MummyBodyComp.kinematicCC.BaseVelocity);
            Vector3 localVelocity = transform.InverseTransformDirection(normalizedVelocity);
            if (MummyBodyComp.IsEmerging)
            {
                ModelAnimation = "Emerge";
            }
            else if (MummyBodyComp.kinematicCC.BaseVelocity.magnitude != 0)
            {
                ModelAnimation = "Move Forward";
                Vector3 horizontalVelocity = MummyBodyComp.kinematicCC.BaseVelocity - new Vector3(0, MummyBodyComp.kinematicCC.BaseVelocity.y, 0);
                AnimationSpeed = (horizontalVelocity.magnitude / MummyBodyComp.moveSpeed) * (localVelocity.z < 0 ? -1 : 1);
            }
            else if (MummyBodyComp.kinematicCC.BaseVelocity.magnitude == 0)
                ModelAnimation = "Idle";
        }
        if (ModelAnimation != oldModelAnimation)
            ModelAnimator.CrossFadeInFixedTime(ModelAnimation, crossFadeTime, 0);
        ModelAnimator.SetFloat("movespeed", AnimationSpeed);
    }

    public void HandleAttack(string animName)
    {
        switch (animName)
        {
            case "swipe":
                {
                    ModelAnimator.Play("Attack", 1, 0);
                    break;
                }
        }
    }
}
