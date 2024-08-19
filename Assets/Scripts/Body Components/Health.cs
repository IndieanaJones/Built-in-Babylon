using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public int MaxHealth = 10;
    public int CurrentHealth = 10;
    public bool DeathCalledFor = false;

    public BaseBody BaseBodyComp;

    public string HurtSound;
    public string DeathSound;

    public void Start()
    {
        if (BaseBodyComp != null)
            BaseBodyComp = GetComponent<BaseBody>();
    }

    public void Update()
    {
        if(CurrentHealth == 0)
        {
            DeathCalledFor = true;
            GetComponent<BaseBody>().Die();
        }
    }

    public void Heal(int healAmount)
    {
        CurrentHealth = Mathf.Min(CurrentHealth + healAmount, MaxHealth);
    }

    public void TakeDamage(int damageAmount)
    {
        CurrentHealth = Mathf.Max(0, CurrentHealth - damageAmount);
        if (CurrentHealth != 0)
            BaseBodyComp.PlaySound(HurtSound, 0.0f);
        else
            AudioSource.PlayClipAtPoint((AudioClip)Resources.Load("Sounds/" + DeathSound), transform.position, 2);
    }
}
