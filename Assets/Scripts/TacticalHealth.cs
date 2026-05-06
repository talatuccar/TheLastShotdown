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
        
        if (anim == null) anim = GetComponentInChildren<Animator>();
    }

   
    public void TakeDamage(float damage, Vector3 hitPoint)
    {
        ProcessShot(damage, false, hitPoint);
    }

    // Hitbox scriptinden doðrudan çaðrýlan ana metod (Kafa/Vücut ayrýmý yapar)
    public void ProcessShot(float damage, bool isHeadshot, Vector3 hitPoint)
    {
        if (isDead) return;

        if (isHeadshot)
        {
            currentHealth = 0;
            Debug.Log("<color=red>TACTICAL HEADSHOT!</color>");
        }
        else
        {
            currentHealth -= damage;
        }

        
        if (bloodEffectPrefab != null)
        {
            GameObject blood = Instantiate(bloodEffectPrefab, hitPoint, Quaternion.identity);
            Destroy(blood, 2f);
        }

      
        if (currentHealth <= 0)
        {
            Die(isHeadshot);
        }
    }

    private void Die(bool wasHeadshot)
    {
        if (isDead) return; 
        isDead = true;

       
        TacticalEnemy tacticalScript = GetComponent<TacticalEnemy>();
        if (tacticalScript != null) tacticalScript.enabled = false;

        
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

     
       
        Destroy(gameObject, 5f); 
    }
}