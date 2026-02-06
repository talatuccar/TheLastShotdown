using UnityEngine;

public class BreakableBox : MonoBehaviour
{
    public BreakableDataSo breakableData; 
    private float currentHealth;
   
    void Start()
    {
        if (breakableData != null)
            currentHealth = breakableData.maxHealth;
    }

    public void TakeDamage(float amount)
    {
        currentHealth -= amount;
        if (currentHealth <= 0)
        {
            Break();
        }
    }

    void Break()
    {
        // Kýrýlmýþ modeli oluþtur
        if (breakableData.brokenPrefab != null)
        {
            GameObject broken = Instantiate(breakableData.brokenPrefab, transform.position, transform.rotation);
           
            Destroy(broken,5f);
        }

       

        int random = Random.Range(0, breakableData.lootPrefabs.Length);
        if (breakableData.lootPrefabs[random] != null)
        {

            Instantiate(breakableData.lootPrefabs[random], transform.position + Vector3.up * 1f, Quaternion.identity);

        }
        Destroy(gameObject);
    }
}