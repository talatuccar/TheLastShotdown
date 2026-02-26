using AI.FSM; // State Machine klasörün/namespace'in
using UnityEngine;

public class EnemyHealth : MonoBehaviour, IDamageable
{
    [Header("Health Config")]
    [SerializeField] private float maxHealth = 100f;
    private float _currentHealth;
    private bool _isDead = false;

    [Header("Visuals & Effects")]
    public GameObject bloodEffectPrefab; 

    private EnemyController _enemy;

    private void Awake()
    {
        _currentHealth = maxHealth;
        _enemy = GetComponent<EnemyController>();
    }


    public void ProcessHit(float amount, bool isHeadshot, Vector3 hitPoint)
    {
        if (_isDead) return;

        _currentHealth -= amount;

        // Efekt oluþturma
        if (bloodEffectPrefab != null)
        {
            Instantiate(bloodEffectPrefab, hitPoint, Quaternion.identity);
        }

        if (_currentHealth <= 0)
        {
            Die(isHeadshot);
        }
    }

  
    public void TakeDamage(float amount, Vector3 hitPoint)
    {
        ProcessHit(amount, false, hitPoint);
    }

    private void Die(bool headshot)
    {
        if (_isDead) return;
        _isDead = true;

        Debug.Log(headshot ? "<color=red>HEADSHOT!</color>" : "<color=black>NPC Öldü!</color>");

        DeathType type = headshot ? DeathType.Headshot : DeathType.General;
        _enemy.ChangeState(new DeathState(_enemy, type));

        _enemy.ShowPasswordDigit();

        Destroy(gameObject, 5f);
    }











    //public void TakeDamage(float amount, Vector3 hitPoint)
    //{
    //    if (_isDead) return;

    //    _currentHealth -= amount;

      
    //    if (bloodEffectPrefab != null)
    //    {
    //        Instantiate(bloodEffectPrefab, hitPoint, Quaternion.identity);
    //    }

    //    if (_currentHealth <= 0)
    //    {
    //        Die();
    //    }
    //}

   

    //private void Die()
    //{
    //    if (_isDead) return;
    //    _isDead = true;

    //    Debug.Log("<color=black>NPC Öldü!</color>");

    //    // State Machine'i Ölüm durumuna geçiriyoruz
    //    // Bu sayede hareket durur, animasyon oynar, beyin kapanýr.
    //    _enemy.ChangeState(new DeathState(_enemy));

    //    // Cesedi 5 saniye sonra sahneden temizle
    //    Destroy(gameObject, 5f);
    //}
}