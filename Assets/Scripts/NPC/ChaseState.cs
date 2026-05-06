using AI.FSM;
using UnityEngine;

public class ChaseState : IState
{
    private EnemyController _enemy;
    private float _fireRate = 0.9f;
    private float _nextFireTime;
    public ChaseState(EnemyController enemy) => _enemy = enemy;

    public void OnEnter()
    {      
        _enemy.agent.speed = _enemy.enemyData.chaseSpeed;     
    }

    public void OnUpdate()
    {
        if (_enemy.Player != null)
        {
            _enemy.agent.SetDestination(_enemy.Player.position);
            _enemy.anim.SetFloat("Speed", _enemy.agent.velocity.magnitude);
            
                if (Time.time >= _nextFireTime)
                {

                    _enemy.Shoot();
                    _nextFireTime = Time.time + _fireRate;
                }
            
            float distance = Vector3.Distance(_enemy.transform.position, _enemy.Player.position);

           
            if (distance <= _enemy.enemyData.attackRange)
            {
                _enemy.ChangeState(new AttackState(_enemy));
            }
        }
    }
    public void OnExit() { }
}