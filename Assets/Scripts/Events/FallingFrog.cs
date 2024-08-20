using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingFrog : MonoBehaviour
{
    public GameObject Frog;
    public GameObject Indicator;
    public Collider DamageZone;
    public ParticleSystem Particles;

    public AudioSource AudioSourceComp;

    public float TimeSummoned;
    public int Stage = 0;

    public List<Health> PeopleDamaged;

    public void Start()
    {
        TimeSummoned = Time.time;
    }

    public void Update()
    {
        ProcessFallingFrog();
    }

    public void ProcessFallingFrog()
    {
        float Age = Time.time - TimeSummoned;
        if (Stage == 0)
        {
            Indicator.SetActive(true);
            Frog.transform.localPosition = Vector3.Lerp(new Vector3(0, 600, 0), new Vector3(0, 0, 0), Age / 4);
            if(Age > 4f)
            {
                Stage++;
                AudioSourceComp.Play();
                Particles.Play();
            }
        }
        if (Stage == 1)
        {
            Frog.SetActive(false);
            Indicator.SetActive(false);
            DamageZone.enabled = true;
            if (Age > 5f)
                Stage++;
        }
        if (Stage == 2)
            Destroy(gameObject);
    }

    public void OnTriggerStay(Collider other)
    {
        if(other.GetComponent<Health>() && !PeopleDamaged.Contains(other.GetComponent<Health>()))
        {
            PeopleDamaged.Add(other.GetComponent<Health>());
            other.GetComponent<Health>().TakeDamage(2);
        }
    }
}
