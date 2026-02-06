using System;
using UnityEngine;

public class HealthItem : BaseLoot
{
    //public Health_UI health_ui;
    public static event Action<int> OnHealthChanged;
    const int healthIncrease = 50;
    int currentHealth;
    protected override void Collect()
    {
        base.Collect();
        
        currentHealth = PlayerInventory.Instance.AddHealth(healthIncrease);
        OnHealthChanged?.Invoke(currentHealth);
        
    }

}
