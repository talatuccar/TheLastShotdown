using AI.FSM; // State Machine klasörün/namespace'in
using UnityEngine;

public class EnemyHealth : MonoBehaviour, IDamageable
{
    [Header("Health Config")]
    [SerializeField] private float maxHealth = 100f;
    private float _currentHealth;
    private bool _isDead = false;

    [Header("Visuals & Effects")]
    public GameObject bloodEffectPrefab; // Vurulduðunda çýkacak kan

    private EnemyController _enemy;

    private void Awake()
    {
        _currentHealth = maxHealth;
        _enemy = GetComponent<EnemyController>();
    }

    // WeaponBase'deki Raycast bu metodu çaðýracak
    // EnemyHealth.cs içinde
    public void TakeDamage(float amount, Vector3 hitPoint)
    {
        if (_isDead) return;

        _currentHealth -= amount;

        // --- ÝÞTE BURASI! ---
        // Efekti düþmanýn merkezinde deðil, merminin vurduðu noktada oluþtur.
        if (bloodEffectPrefab != null)
        {
            Instantiate(bloodEffectPrefab, hitPoint, Quaternion.identity);
        }

        if (_currentHealth <= 0)
        {
            Die();
        }
    }

    private void SpawnBloodEffect()
    {
        if (bloodEffectPrefab != null)
        {
            // Kaný karakterin biraz yukarýsýnda (gövde hizasý) oluþtur
            Instantiate(bloodEffectPrefab, transform.position + Vector3.up * 1.2f, Quaternion.identity);
        }
    }

    private void Die()
    {
        if (_isDead) return;
        _isDead = true;

        Debug.Log("<color=black>NPC Öldü!</color>");

        // State Machine'i Ölüm durumuna geçiriyoruz
        // Bu sayede hareket durur, animasyon oynar, beyin kapanýr.
        _enemy.ChangeState(new DeathState(_enemy));

        // Cesedi 5 saniye sonra sahneden temizle
        Destroy(gameObject, 5f);
    }
}