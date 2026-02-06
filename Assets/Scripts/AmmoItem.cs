using System;
using UnityEngine;

public class AmmoItem : BaseLoot
{

    public static event Action<int> OnAmmoChanged;
    const int ammoIncrease = 30;
    int currentAmmo;
    protected override void Collect()
    {
        base.Collect();

        currentAmmo = PlayerInventory.Instance.AddAmmo(ammoIncrease);
        OnAmmoChanged?.Invoke(currentAmmo);
        
    }
    
}
