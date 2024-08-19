using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BodyFallApart : MonoBehaviour
{
    public List<GameObject> BodyParts;

    public void fallApart()
    {
        foreach (GameObject part in BodyParts)
        {
            BoxCollider boxCollider = part.gameObject.AddComponent(typeof(BoxCollider)) as BoxCollider;
            Rigidbody rigidbody = part.gameObject.AddComponent(typeof(Rigidbody)) as Rigidbody;
            Vector3 velocity = new Vector3(UnityEngine.Random.Range(-120.0f, 120.0f), UnityEngine.Random.Range(0.0f, 500.0f), UnityEngine.Random.Range(-120.0f, 120.0f));
            rigidbody.AddForce(velocity);
        }
    }
}
