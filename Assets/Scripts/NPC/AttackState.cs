using AI.FSM;
using UnityEngine;

public class AttackState : IState
{
    private EnemyController _enemy;
    private float _fireRate = 1.2f; 
    private float _nextFireTime;

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
        if (_enemy.player == null)
        {
            _enemy.GoBackToPreviousState();
            return;
        }


        Vector3 direction = (_enemy.player.position - _enemy.transform.position).normalized;
        direction.y = 0; 
        _enemy.transform.rotation = Quaternion.Slerp(_enemy.transform.rotation, Quaternion.LookRotation(direction), Time.deltaTime * 10f);

        // Mesafe Kontrolü: Oyuncu çok uzaklaşırsa tekrar koşmaya başla
        float distance = Vector3.Distance(_enemy.transform.position, _enemy.player.position);
        if (distance > _enemy.data.attackRange + 1.5f)
        {
            _enemy.GoBackToPreviousState(); // Chase durumuna geri döner
            return;
        }

        
        if (Time.time >= _nextFireTime)
        {
            Shoot();
            _nextFireTime = Time.time + _fireRate;
        }
    }

    private void Shoot()
    {
        Debug.Log("Düşman Ateş Ediyor! 🔫");
        
    }

    public void OnExit()
    {
        _enemy.agent.isStopped = false;
        _enemy.anim.SetBool("isAttacking", false);
    }
}