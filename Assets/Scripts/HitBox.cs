using UnityEngine;

public class Hitbox : MonoBehaviour, IDamageable
{
    public EnemyHealth healthSystem; // Inspector'dan sürükle
    public float damageMultiplier = 1f;
    public bool isHeadshot = false;

    public void TakeDamage(float damage, Vector3 hitPoint)
    {
        // Doðrudan Health scriptindeki yeni metodumuzu çaðýrýyor
        healthSystem.ProcessHit(damage * damageMultiplier, isHeadshot, hitPoint);
    }
}