using System;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public static PlayerInventory Instance;
    public WeaponBase currentWeapon;
    public PlayerInventorySo playerInventoryDataSo;
    public static event Action<int> OnHealthDataChanged;
    

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);

        Reset();
    }
    public void DecreaseHealth(int amount)
    {
        playerInventoryDataSo.HealtAmount -= amount;

        OnHealthDataChanged?.Invoke(playerInventoryDataSo.HealtAmount);
    }


    public void AddHealth(int amount)
    {
        playerInventoryDataSo.HealtAmount += amount;

        OnHealthDataChanged?.Invoke(playerInventoryDataSo.HealtAmount);
    }
    public int DecreaseAmmo()
    {
        if (currentWeapon.weaponData.currentAmmo > 0)
        {
            currentWeapon.weaponData.currentAmmo--;
        }
        return currentWeapon.weaponData.currentAmmo;
    }

    public int AddAmmo(int increaseAmmo)
    {

        return currentWeapon.weaponData.currentAmmo += increaseAmmo;
    }
    public int CurrentAmmo()
    {
        return currentWeapon.weaponData.currentAmmo;
    }
    public int InitialHeath()
    {

        return playerInventoryDataSo.HealtAmount;
    }

    public int GetMaxAmmo()
    {

        return currentWeapon.weaponData.maxAmmo;
    }

    public void Reset()
    {
        playerInventoryDataSo.HealtAmount = 100;
        //playerInventoryDataSo.AmmoAmount = 200;
    }
}
