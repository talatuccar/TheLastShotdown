using TMPro;
using UnityEngine;

public class Ammo_UI : MonoBehaviour
{
    public TextMeshProUGUI Current_ammo_UI;

    public void UpdateAmmo(int currentAmmo)
    {

        Current_ammo_UI.text = currentAmmo.ToString();
    }
    

    public void OnShooted()
    {
        Current_ammo_UI.text = PlayerInventory.Instance.DecreaseAmmo().ToString();
    }

    void Start()
    {
        Current_ammo_UI.text = PlayerInventory.Instance.InitialAmmo().ToString();
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
