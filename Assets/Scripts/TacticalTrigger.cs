using UnityEngine;

public class TacticalTrigger : MonoBehaviour
{
    public TacticalEnemy tacticalEnemy;
    private BoxCollider col;

    void Start()
    {
        col = GetComponent<BoxCollider>();
        if (col != null && tacticalEnemy != null)
        {
            // NPC'ye kutunun boyutuna göre mesafe hesaplatýyoruz
            tacticalEnemy.InitializeDistance(col.size.x * transform.localScale.x,
                                            col.size.z * transform.localScale.z,
                                            transform.position);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) tacticalEnemy.SetPlayerInZone(true);
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player")) tacticalEnemy.SetPlayerInZone(false);
    }
}