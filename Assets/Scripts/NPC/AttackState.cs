using AI.FSM;
using System;
using UnityEngine;


public class AttackState : IState
{
    private EnemyController _enemy;
    private float _fireRate = 0.9f;
    private float _nextFireTime;
    public WeaponDataSo weaponData;
    public static Action<float> OnPlayerShooted;
    public AttackState(EnemyController enemy) => _enemy = enemy;

    public void OnEnter()
    {
        _enemy.agent.isStopped = true;
        _enemy.agent.velocity = Vector3.zero;
        _enemy.anim.SetFloat("Speed", 0);
        _enemy.anim.SetBool("isAttacking", true);
    }

    public void OnUpdate()
    {
        if (_enemy.Player == null)
        {
            _enemy.GoBackToPreviousState();
            return;
        }

        LookAtPlayer();

        // Sadece ateş etme zamanı geldiğinde ateş et
        if (Time.time >= _nextFireTime)
        {
            // Eğer CanSeePlayer kontrolü yapmak istersen buraya ekleyebilirsin
            _enemy.Shoot();
            _nextFireTime = Time.time + _fireRate;
        }
        // BURADAKİ ELSE BLOĞUNU SİLDİK! 
        // Ateş edemediği sürede (bekleme süresinde) State'te kalmaya devam etmeli.

        // Sadece mesafe çok açılırsa State'ten çık
        float distance = Vector3.Distance(_enemy.transform.position, _enemy.Player.position);
        if (distance > _enemy.enemyData.attackRange + 1.5f)
        {
            _enemy.GoBackToPreviousState();
            return;
        }
    }

    private void LookAtPlayer()
    {
        Vector3 direction = (_enemy.Player.position - _enemy.transform.position).normalized;
        direction.y = 0;

        if (direction != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            _enemy.transform.rotation = Quaternion.Slerp(_enemy.transform.rotation, targetRotation, Time.deltaTime * 10f);
        }
    }

    //private void Shoot()
    //{
    //    if (_enemy.muzzleFlashParticle != null)
    //    {
    //        _enemy.muzzleFlashParticle.Play();  


    //    }
    //    SoundManager.Instance.PlayAudioClip(_enemy.enemyData.enemyfireSound);
    //    int randomHealthDecrease = UnityEngine.Random.Range(0, 10);
    //    PlayerInventory.Instance.DecreaseHealth(randomHealthDecrease);
    //}

    public void OnExit()
    {
        _enemy.agent.isStopped = false;
        _enemy.anim.SetBool("isAttacking", false);
    }
}