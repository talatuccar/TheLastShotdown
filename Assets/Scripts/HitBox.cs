using UnityEngine;
public class Hitbox : MonoBehaviour, IDamageable
{
    private EnemyHealth generalHealthSystem;
    private TacticalHealth tacticalHealthSystem;

    public float damageMultiplier = 1f;
    public bool isHeadshot = false;

    private void Awake()
    {
        generalHealthSystem = GetComponentInParent<EnemyHealth>();
        tacticalHealthSystem = GetComponentInParent<TacticalHealth>();
    }

    public void TakeDamage(float damage, Vector3 hitPoint)
    {
       
        if (generalHealthSystem != null)
        {
            generalHealthSystem.ProcessHit(damage * damageMultiplier, isHeadshot, hitPoint);
        }
        else if (tacticalHealthSystem != null)
        {
            tacticalHealthSystem.ProcessShot(damage * damageMultiplier, isHeadshot, hitPoint);
        }
        
    }
}