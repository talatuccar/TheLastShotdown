using UnityEngine;

public class Hitbox : MonoBehaviour, IDamageable
{
    public EnemyHealth healthSystem; 
    public float damageMultiplier = 1f;
    public bool isHeadshot = false;

    public void TakeDamage(float damage, Vector3 hitPoint)
    {
        
        healthSystem.ProcessHit(damage * damageMultiplier, isHeadshot, hitPoint);
    }
}