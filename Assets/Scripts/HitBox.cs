//using UnityEngine;

//public class Hitbox : MonoBehaviour, IDamageable
//{
//    public EnemyHealth healthSystem; 
//    public float damageMultiplier = 1f;
//    public bool isHeadshot = false;

//    public void TakeDamage(float damage, Vector3 hitPoint)
//    {

//        healthSystem.ProcessHit(damage * damageMultiplier, isHeadshot, hitPoint);
//    }
//}

using UnityEngine;

public class Hitbox : MonoBehaviour, IDamageable
{
    private EnemyHealth generalHealthSystem;
    private TacticalHealth tacticalHealthSystem;

    public float damageMultiplier = 1f;
    public bool isHeadshot = false;

    private void Awake()
    {
        // Bu fonksiyon hiyerarþide yukarý doðru týrmanýr 
        // ve bulduðu ÝLK saðlýk scriptine yapýþýr. 
        // Yani 8 NPC'yi tutan o en üstteki boþ objeye kadar gitmez, 
        // kendi NPC'sinin gövdesinde durur.
        generalHealthSystem = GetComponentInParent<EnemyHealth>();
        tacticalHealthSystem = GetComponentInParent<TacticalHealth>();
    }

    public void TakeDamage(float damage, Vector3 hitPoint)
    {
        // Önce Taktiksel mi diye bak (Çünkü yeni sistemin bu)
        if (generalHealthSystem != null)
        {
            generalHealthSystem.ProcessHit(damage * damageMultiplier, isHeadshot, hitPoint);
        }
        // Deðilse normal kovalayan mý diye bak
        else if (tacticalHealthSystem != null)
        {
            tacticalHealthSystem.ProcessShot(damage * damageMultiplier, isHeadshot, hitPoint);
        }
        
    }
}