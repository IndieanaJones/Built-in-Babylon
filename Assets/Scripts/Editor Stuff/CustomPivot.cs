using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomPivot : MonoBehaviour
{
    //How big our pivot should appear
    public float pivotSize = 0.25f;
    //What color should the pivot be?
    public Color pivotColor = Color.yellow;

    //Makes the yellow circles in the editor
    void OnDrawGizmos()
    {
        Gizmos.color = pivotColor;
        Gizmos.DrawWireSphere(transform.position, pivotSize);
    }
}
