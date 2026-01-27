using UnityEngine;

public class BreakableBox : MonoBehaviour
{
    public BreakableDataSo data; // Oluþturduðun SO'yu buraya sürükleyeceksin
    private float currentHealth;

    void Start()
    {
        if (data != null)
            currentHealth = data.maxHealth;
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
        if (data.brokenPrefab != null)
        {
            GameObject broken = Instantiate(data.brokenPrefab, transform.position, transform.rotation);
            // Parçalarýn biraz daðýlmasý için (Opsiyonel)
            Destroy(broken,5f);
        }

        // Loot çýkart

        int random = Random.Range(0, data.lootPrefabs.Length);
        if (data.lootPrefabs[random] != null)
        {

            Instantiate(data.lootPrefabs[random], transform.position + Vector3.up * 1f, Quaternion.identity);

        }
        Destroy(gameObject);
    }
}