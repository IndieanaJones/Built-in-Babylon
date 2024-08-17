using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class FootGuider : MonoBehaviour
{
    //Our first pivot.
    public Transform thighPivotPosition;
    //Our second pivot.
    public Transform calfPivotPosition;
    //Our third pivot.
    public Transform footPivotPosition;
    //Whether or not we should be currently applying this.
    public  bool doCalculations = true;
    //Our pivot tap
    public GameObject ourPivotTap;

    //The base of the model
    public Transform BaseModelTransform;

    //The three lengths of the sides of our "triangle"
    private float _distanceFromThighToCalf = 0.0f;
    private float _distanceFromCalfToFoot = 0.0f;
    private float _distanceFromThighToGhostFoot = 0.0f;

    public void Start()
    {
    }

    public void Update()
    {
        ourPivotTap.transform.position = new Vector3(thighPivotPosition.position.x, ourPivotTap.transform.position.y, thighPivotPosition.position.z);
    }

    public void LateUpdate()
    {
        if (!doCalculations)
            return;

        //We can't calculate this in Start() because characters might dynamically change in size.
        _distanceFromThighToCalf = Vector3.Distance(thighPivotPosition.position, (calfPivotPosition.position));
        _distanceFromCalfToFoot = Vector3.Distance(calfPivotPosition.position, footPivotPosition.position);
        //And we can't calculate this in Start() because this is always changing. Note this goes to the ghost foot, because the ghost foot position is our wanted foot position.
        _distanceFromThighToGhostFoot = Vector3.Distance(thighPivotPosition.position, transform.position);

        float xBendA = Vector2.Distance(Vector2.zero, new Vector2(transform.localPosition.y * BaseModelTransform.localScale.y, transform.localPosition.x * BaseModelTransform.localScale.x));
        float xBendB = _distanceFromThighToCalf + _distanceFromCalfToFoot;
        float xBendC = Vector2.Distance(new Vector2(-xBendB, 0), new Vector2(transform.localPosition.y * BaseModelTransform.localScale.y, transform.localPosition.x * BaseModelTransform.localScale.x));

        float requiredXBend = Mathf.Acos((Mathf.Pow(xBendC, 2) - Mathf.Pow(xBendA, 2) - Mathf.Pow(xBendB, 2)) / (-2 * xBendA * xBendB));
        requiredXBend = requiredXBend * Mathf.Rad2Deg * (transform.localPosition.x < 0 ? 1 : -1);

        float zBendA = Vector2.Distance(Vector2.zero, new Vector2(transform.localPosition.y * BaseModelTransform.localScale.y, transform.localPosition.z * BaseModelTransform.localScale.z));
        float zBendB = _distanceFromThighToCalf + _distanceFromCalfToFoot;
        float zBendC = Vector2.Distance(new Vector2(-zBendB, 0), new Vector2(transform.localPosition.y * BaseModelTransform.localScale.y, transform.localPosition.z * BaseModelTransform.localScale.z));

        float requiredZBend = Mathf.Acos((Mathf.Pow(zBendC, 2) - Mathf.Pow(zBendA, 2) - Mathf.Pow(zBendB, 2)) / (-2 * zBendA * zBendB));
        requiredZBend = requiredZBend * Mathf.Rad2Deg * (transform.localPosition.z < 0 ? -1 : 1);

        ourPivotTap.transform.rotation = Quaternion.Euler(new Vector3(0, thighPivotPosition.eulerAngles.y, 0));

        if (_distanceFromThighToGhostFoot > _distanceFromThighToCalf + _distanceFromCalfToFoot)
        {
            return;
        }

        float angleForUpperLeg = Mathf.Acos((Mathf.Pow(_distanceFromCalfToFoot, 2) - Mathf.Pow(_distanceFromThighToGhostFoot, 2) - Mathf.Pow(_distanceFromThighToCalf, 2)) / (-2 * _distanceFromThighToGhostFoot * _distanceFromThighToCalf));
        angleForUpperLeg = -(angleForUpperLeg * Mathf.Rad2Deg);


        float angleForLowerLeg = Mathf.Acos((Mathf.Pow(_distanceFromThighToGhostFoot, 2) - Mathf.Pow(_distanceFromCalfToFoot, 2) - Mathf.Pow(_distanceFromThighToCalf, 2)) / (-2 * _distanceFromCalfToFoot * _distanceFromThighToCalf));
        angleForLowerLeg = angleForLowerLeg * 180 / Mathf.PI;

        if (float.IsNaN(requiredXBend))
            requiredXBend = 0;
        if (float.IsNaN(requiredZBend))
            requiredZBend = 0;
        if (float.IsNaN(angleForUpperLeg))
            angleForUpperLeg = 0;

        thighPivotPosition.rotation = Quaternion.Euler(angleForUpperLeg + requiredZBend, thighPivotPosition.rotation.eulerAngles.y, requiredXBend);
        calfPivotPosition.localRotation = Quaternion.Euler(180 - angleForLowerLeg, 0, 0);
        footPivotPosition.rotation = transform.rotation;
    }
}
