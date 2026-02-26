using AI.FSM;
using UnityEngine;

public class ChaseState : IState
{
    private EnemyController _enemy;

    public ChaseState(EnemyController enemy) => _enemy = enemy;

    public void OnEnter()
    {
        
        _enemy.agent.speed = _enemy.enemyData.chaseSpeed;
        Debug.Log("Seni gördüm! Geliyorum!");
    }

    public void OnUpdate()
    {
        if (_enemy.Player != null)
        {
            _enemy.agent.SetDestination(_enemy.Player.position);
            _enemy.anim.SetFloat("Speed", _enemy.agent.velocity.magnitude);

            float distance = Vector3.Distance(_enemy.transform.position, _enemy.Player.position);

           
            if (distance <= _enemy.enemyData.attackRange)
            {
                _enemy.ChangeState(new AttackState(_enemy));
            }
        }
    }
    public void OnExit() { }
}