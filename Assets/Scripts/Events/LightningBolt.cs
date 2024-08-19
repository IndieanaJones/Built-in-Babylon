using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightningBolt : MonoBehaviour
{
    public GameObject Cloud;
    public GameObject Bolt;
    public GameObject Indicator;
    public Collider DamageZone;
    public ParticleSystem Particles;

    public AudioSource AudioSourceComp;

    public float TimeSummoned;
    public int Stage = 0;

    public List<Health> PeopleDamaged;

    public void Start()
    {
        Cloud.transform.localScale = new Vector3(0, 0, 0);
        Bolt.SetActive(false);
        TimeSummoned = Time.time;
        CheckForRoof();
    }

    public void Update()
    {
        ProcessLightningBolt();
    }

    public void CheckForRoof()
    {
        GameObject target = PlayerSpawner.ThePlayerRef;
        RaycastHit hit;
        if(Physics.Raycast(target.GetComponent<BaseBody>().ourCamera.transform.position, Vector3.up, out hit, 200))
        {
            if(hit.collider != null)
            {
                Destroy(gameObject);
            }
        }
    }

    public void ProcessLightningBolt()
    {
        float Age = Time.time - TimeSummoned;
        if (Stage == 0)
        {
            Indicator.SetActive(true);
            Cloud.transform.localScale = new Vector3(Age / 1.5f, Age / 1.5f, Age / 1.5f);
            if(Age > 1.5f)
            {
                Stage++;
                AudioSourceComp.Play();
                Particles.Play();
            }
            Bolt.SetActive(false);
        }
        if (Stage == 1)
        {
            Indicator.SetActive(true);
            DamageZone.enabled = true;
            Cloud.transform.localScale = new Vector3(1, 1, 1);
            Bolt.SetActive(true);
            if (Age > 2f)
                Stage++;
        }
        if (Stage == 2)
        {
            Bolt.SetActive(false);
            DamageZone.enabled = false;
            Indicator.SetActive(false);
            Cloud.transform.localScale = new Vector3(1 - (Age - 2), 1 - (Age - 2), 1 - (Age - 2));
            if (Age >= 3f)
                Stage++;
        }
        if (Stage == 3)
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
