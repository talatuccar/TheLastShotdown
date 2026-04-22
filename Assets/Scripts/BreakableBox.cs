using UnityEngine;

public class BreakableBox : MonoBehaviour, IDamageable
{
    public BreakableDataSo breakableData;
    private float currentHealth;

    void Start()
    {
        if (breakableData != null)
            currentHealth = breakableData.maxHealth;
    }

    public void TakeDamage(float amount, Vector3 hitPoint)
    {
        currentHealth -= amount;
        if (currentHealth <= 0)
        {
            Break();
        }
    }

    void Break()
    {

        if (breakableData.brokenBoxPrefab != null)
        {
            EffectPooler.Instance.SpawnFromPool(breakableData.breakableBoxPoolTag, transform.position, transform.rotation);
        }


        int random = Random.Range(0, breakableData.lootPrefabs.Length);
        if (breakableData.lootPrefabs[random] != null)
        {

            Instantiate(breakableData.lootPrefabs[random], transform.position + Vector3.up * 1f, Quaternion.identity);

        }
        Destroy(gameObject);
    }
}