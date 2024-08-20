using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirectCameraSound : MonoBehaviour
{
    public AudioSource AudioSourceComp;
    public static DirectCameraSound Instance;

    public void Awake()
    {
        Instance = this;
    }

    public void PlaySound(string soundName, float variance = 0)
    {
        AudioSourceComp.pitch = 1 + Random.Range(-variance, variance);
        AudioSourceComp.PlayOneShot((AudioClip)Resources.Load("Sounds/" + soundName));
    }
}
