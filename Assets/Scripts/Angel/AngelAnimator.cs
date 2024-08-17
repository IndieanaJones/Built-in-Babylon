using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AngelAnimator : MonoBehaviour
{
    public GameObject PlayerTarget;
    public GameObject EyePivot;

    public void FixedUpdate()
    {
        if (PlayerTarget == null)
            PlayerTarget = PlayerSpawner.ThePlayerRef;
    }

    public void LateUpdate()
    {
        LookAtPlayer();
    }

    public void LookAtPlayer()
    {
        if (PlayerTarget == null)
            return;
        transform.LookAt(PlayerTarget.transform, Vector3.up);
        Vector3 necessaryRotations = transform.rotation.eulerAngles;
        transform.rotation = Quaternion.identity;
        EyePivot.transform.rotation = Quaternion.Euler(necessaryRotations.x, 0, 0);
        transform.rotation = Quaternion.Euler(0, necessaryRotations.y, 0);
    }
}
