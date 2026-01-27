using UnityEngine;

public class HealthItem : BaseLoot, ICollectable
{
    public int amount = 25;

    public void Collect()
    {
        Debug.Log(amount + " Can eklendi.");
        
        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) Collect();
    }

}
