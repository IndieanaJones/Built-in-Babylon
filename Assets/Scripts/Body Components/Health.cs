using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public int MaxHealth = 10;
    public int CurrentHealth = 10;
    public bool DeathCalledFor = false;

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
    }
}
