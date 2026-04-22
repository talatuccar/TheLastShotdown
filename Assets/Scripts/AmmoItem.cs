using System;

public class AmmoItem : BaseLoot
{

    public static event Action<int> OnAmmoChanged;
    const int ammoIncrease = 30;
   
    protected override void Collect()
    {
        base.Collect();

        int currentAmmo = PlayerInventory.Instance.AddAmmo(ammoIncrease);
        OnAmmoChanged?.Invoke(currentAmmo);
        
    }
    
}
