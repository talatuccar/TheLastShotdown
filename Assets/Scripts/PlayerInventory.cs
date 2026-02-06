using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public static PlayerInventory Instance;

    public PlayerInventorySo playerInventoryDataSo;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);

        Reset();
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

    public int AddHealth(int increaseHealth)
    {

        return playerInventoryDataSo.HealtAmount += increaseHealth;
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
        playerInventoryDataSo.AmmoAmount = 30;
    }
}
