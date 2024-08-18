using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuilderAnimator : MonoBehaviour
{
    public BuilderBody BuilderBodyComp;

    public Animator ModelAnimator;

    public string ModelAnimation = "";
    public float AnimationSpeed = 1;

    public Animator FPSAnimator;
    public string FPSAnimation = "";

    public void Update()
    {
        UpdateModelAnimator();
        UpdateFPSAnimator();
    }

    public void UpdateModelAnimator()
    {
        string oldModelAnimation = ModelAnimation;
        AnimationSpeed = 1;
        float crossFadeTime = 0.1f;
        {
            Vector3 normalizedVelocity = Vector3.Normalize(BuilderBodyComp.kinematicCC.BaseVelocity);
            Vector3 localVelocity = transform.InverseTransformDirection(normalizedVelocity);
            if (BuilderBodyComp.isChiseling)
            {
                ModelAnimation = "Chisel";
            }
            else if (BuilderBodyComp.kinematicCC.BaseVelocity.magnitude != 0)
            {
                ModelAnimation = "Move Forward";
                Vector3 horizontalVelocity = BuilderBodyComp.kinematicCC.BaseVelocity - new Vector3(0, BuilderBodyComp.kinematicCC.BaseVelocity.y, 0);
                AnimationSpeed = (horizontalVelocity.magnitude / BuilderBodyComp.moveSpeed) * (localVelocity.z < 0 ? -1 : 1);
            }
            else if (BuilderBodyComp.kinematicCC.BaseVelocity.magnitude == 0)
                ModelAnimation = "Idle";
        }
        if (ModelAnimation != oldModelAnimation)
            ModelAnimator.CrossFadeInFixedTime(ModelAnimation, crossFadeTime, 0);
        ModelAnimator.SetFloat("movespeed", AnimationSpeed);
    }

    public void UpdateFPSAnimator()
    {
        string oldModelAnimation = FPSAnimation;
        AnimationSpeed = 1;
        float crossFadeTime = 0.1f;
        Vector3 normalizedVelocity = Vector3.Normalize(BuilderBodyComp.kinematicCC.BaseVelocity);
        Vector3 localVelocity = transform.InverseTransformDirection(normalizedVelocity);
        if (BuilderBodyComp.isChiseling)
        {
            FPSAnimation = "Chisel";
        }
        else if (BuilderBodyComp.kinematicCC.BaseVelocity.magnitude != 0)
        {
            FPSAnimation = "Move Forward";
            Vector3 horizontalVelocity = BuilderBodyComp.kinematicCC.BaseVelocity - new Vector3(0, BuilderBodyComp.kinematicCC.BaseVelocity.y, 0);
            AnimationSpeed = (horizontalVelocity.magnitude / BuilderBodyComp.moveSpeed) * (localVelocity.z < 0 ? -1 : 1);
        }
        else if (BuilderBodyComp.kinematicCC.BaseVelocity.magnitude == 0)
            FPSAnimation = "Idle";

        if (FPSAnimation != oldModelAnimation)
            FPSAnimator.CrossFadeInFixedTime(ModelAnimation, crossFadeTime, 0);
        FPSAnimator.SetFloat("movespeed", AnimationSpeed);
    }

    public void HandleAttack(string animName)
    {
        switch (animName)
        {
            case "pickaxe":
                {
                    ModelAnimator.Play("Melee", 1, 0);
                    FPSAnimator.Play("Melee", 1, 0);
                    break;
                }
        }
    }
}
