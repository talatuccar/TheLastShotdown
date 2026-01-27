using UnityEngine;

public class AmmoItem : BaseLoot, ICollectable
{

    public int amount = 30;

    public void Collect()
    {
        Debug.Log(amount + " Mermi eklendi.");
        
        // PlayerInventory.Instance.AddAmmo(amount);
        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) Collect();
    }

}
