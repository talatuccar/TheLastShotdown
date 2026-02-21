using System;
using UnityEngine;

public class HealthItem : BaseLoot
{
    
    const int healthIncrease = 50;
    
    protected override void Collect()
    {
        base.Collect();

        PlayerInventory.Instance.AddHealth(healthIncrease);

    }

}
