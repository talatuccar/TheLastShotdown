using UnityEngine;
using System;
public class PlayerInventory : MonoBehaviour
{
    public static PlayerInventory Instance;

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
        if (playerInventoryDataSo.AmmoAmount > 0)
        {
            playerInventoryDataSo.AmmoAmount--;
        }
        return playerInventoryDataSo.AmmoAmount;
    }

    public int AddAmmo(int increaseAmmo)
    {

        return playerInventoryDataSo.AmmoAmount += increaseAmmo;
    }

    public int InitialHeath()
    {

        return playerInventoryDataSo.HealtAmount;
    }

    public int InitialAmmo()
    {

        return playerInventoryDataSo.AmmoAmount;
    }

    public void Reset()
    {
        playerInventoryDataSo.HealtAmount = 100;
        playerInventoryDataSo.AmmoAmount = 200;
    }
}
