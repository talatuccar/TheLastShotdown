using TMPro;
using UnityEngine;

public class Health_UI : MonoBehaviour
{
    public TextMeshProUGUI Current_health_UI;
    
    public void UpdateHealth(int currentHealth)
    {

        Current_health_UI.text = currentHealth.ToString();
    }
    void Start()
    {
        Current_health_UI.text = PlayerInventory.Instance.InitialHeath().ToString();
    }
    private void OnEnable()
    {
        // Veri dosyasýndaki deðiþikliði dinlemeye baþla
        HealthItem.OnHealthChanged += UpdateHealth;
    }

    private void OnDisable()
    {
        // Obje silinirse dinlemeyi býrak (Memory leak önlemek için)
        HealthItem.OnHealthChanged -= UpdateHealth;
    }
}
