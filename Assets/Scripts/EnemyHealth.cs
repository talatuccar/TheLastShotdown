using AI.FSM; // State Machine klasörün/namespace'in
using UnityEngine;

public class EnemyHealth : MonoBehaviour, IDamageable
{
    [Header("Health Config")]
    [SerializeField] private float maxHealth = 100f;
    private float _currentHealth;
    private bool _isDead = false;

    //[Header("Visuals & Effects")]
    //public GameObject bloodEffectPrefab;

    private EnemyController _enemy;
    private float _nextHitReactionTime;
    [SerializeField] private float hitReactionCooldown = 2f; 

   
    private void Awake()
    {
        _currentHealth = maxHealth;
        _enemy = GetComponent<EnemyController>();
    }


    public void ProcessHit(float amount, bool isHeadshot, Vector3 hitPoint)
    {
        if (_isDead) return;

        _currentHealth -= amount;

        EffectPooler.Instance.SpawnFromPool("Blood", hitPoint, Quaternion.identity);
       

        if (_currentHealth <= 0)
        {
            Die(isHeadshot);
        }


        if (_currentHealth >= 20f && _currentHealth <= 80f)
        {
            PlayHitReaction();
        }
    }

    private void PlayHitReaction()
    {
        if (Time.time < _nextHitReactionTime) return;
        if (_enemy.anim == null) return;

        _enemy.anim.SetTrigger("Hit_Idle");

        //if (_enemy.agent.velocity.magnitude > 0.5f)
        //{
        //    _enemy.anim.SetTrigger("Hit_Running");
        //}
        //else
        //{
        //    _enemy.anim.SetTrigger("Hit_Idle");
        //}
        _nextHitReactionTime = Time.time + hitReactionCooldown;
    }
    public void TakeDamage(float amount, Vector3 hitPoint)
    {
        ProcessHit(amount, false, hitPoint);
    }

    private void Die(bool headshot)
    {
        if (_isDead) return;
        _isDead = true;


        DeathType type;

        if (headshot)
        {
            type = DeathType.Headshot;
        }

        else if (_enemy.agent.velocity.magnitude > 0.5f)
        {
            type = DeathType.ChaseDeath;
        }
        else
        {
            type = DeathType.General;
        }

        _enemy.ChangeState(new DeathState(_enemy, type));

        _enemy.ShowPasswordDigit();

        Destroy(gameObject, 5f);
    }
    
}