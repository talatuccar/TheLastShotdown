using UnityEngine;

public class TacticalHealth : MonoBehaviour, IDamageable
{
    [Header("Saðlýk Ayarlarý")]
    public float maxHealth = 100f;
    private float currentHealth;
    private bool isDead = false;

    [Header("Efektler & Görsel")]
    public Animator anim;
    public GameObject bloodEffectPrefab;

    void Awake()
    {
        currentHealth = maxHealth;
        // Eðer Inspector'dan atanmadýysa otomatik olarak bulur
        if (anim == null) anim = GetComponentInChildren<Animator>();
    }

    // IDamageable arayüzü gereði olmasý gereken metod (Vücut vuruþlarý için)
    public void TakeDamage(float damage, Vector3 hitPoint)
    {
        ProcessShot(damage, false, hitPoint);
    }

    // Hitbox scriptinden doðrudan çaðrýlan ana metod (Kafa/Vücut ayrýmý yapar)
    public void ProcessShot(float damage, bool isHeadshot, Vector3 hitPoint)
    {
        if (isDead) return;

        // KAFADAN VURULMA MANTIÐI: Tek mermide bitirir
        if (isHeadshot)
        {
            currentHealth = 0;
            Debug.Log("<color=red>TACTICAL HEADSHOT!</color>");
        }
        else
        {
            currentHealth -= damage;
        }

        // Kan Partikülü Çýkar
        if (bloodEffectPrefab != null)
        {
            GameObject blood = Instantiate(bloodEffectPrefab, hitPoint, Quaternion.identity);
            Destroy(blood, 2f);
        }

        // Ölüm Kontrolü
        if (currentHealth <= 0)
        {
            Die(isHeadshot);
        }
    }

    private void Die(bool wasHeadshot)
    {
        if (isDead) return; // Çift tetiklenmeyi engelle
        isDead = true;

        // 1. ADIM: Taktiksel hareket/ateþ kodunu anýnda kapat
        TacticalEnemy tacticalScript = GetComponent<TacticalEnemy>();
        if (tacticalScript != null) tacticalScript.enabled = false;

        // 2. ADIM: Ölüm animasyonunu oynat
        if (anim != null)
        {
            if (wasHeadshot)
            {
                anim.SetTrigger("Die_Headshot");
            }
            else
            {
                anim.SetTrigger("Die");
            }
        }

        // 3. ADIM: Collider'larý kapat (Yerdeki cesede mermi çarpmasýn)
        Collider[] allColliders = GetComponentsInChildren<Collider>();
        foreach (var col in allColliders)
        {
            col.enabled = false;
        }

        Debug.Log("Tactical Enemy Etkisiz Hale Getirildi.");
        Destroy(gameObject, 10f); // Ceset 10 saniye sonra yok olsun
    }
}