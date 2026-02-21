using TMPro;
using UnityEngine;

public class Health_UI : MonoBehaviour
{
    public TextMeshProUGUI Current_health_UI;


    void Start()
    {
        Current_health_UI.text = PlayerInventory.Instance.InitialHeath().ToString();
    }
    private void OnEnable()
    {
        PlayerInventory.OnHealthDataChanged += UpdateHealthDisplay;
    }

    private void OnDisable()
    {
        PlayerInventory.OnHealthDataChanged -= UpdateHealthDisplay;
    }

    public void UpdateHealthDisplay(int currentHealth)
    {
        Current_health_UI.text = currentHealth.ToString();
    }
}
