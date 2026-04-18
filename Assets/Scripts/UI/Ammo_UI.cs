using TMPro;
using UnityEngine;

public class Ammo_UI : MonoBehaviour
{
    public TextMeshProUGUI Current_ammo_UI;
    public TextMeshProUGUI total_ammo;
    bool isStart = true;
    public void UpdateAmmo(int currentAmmo)
    {

        Current_ammo_UI.text = currentAmmo.ToString();
    }
    

    public void OnShooted()
    {
        Current_ammo_UI.text = PlayerInventory.Instance.DecreaseAmmo().ToString();
    }

    public void SetInitialAmmo()
    {
        Current_ammo_UI.text = PlayerInventory.Instance.CurrentAmmo().ToString();
        total_ammo.text = PlayerInventory.Instance.GetMaxAmmo().ToString();
    }
    private void OnEnable()
    {
        
        AmmoItem.OnAmmoChanged += UpdateAmmo;
        WeaponBase.OnShooted += OnShooted;
    }

    private void OnDisable()
    {
        
        AmmoItem.OnAmmoChanged -= UpdateAmmo;
        WeaponBase.OnShooted -= OnShooted;
    }
}
