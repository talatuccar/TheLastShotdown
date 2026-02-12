using AI.FSM;
using UnityEngine;

public class ChaseState : IState
{
    private EnemyController _enemy;

    public ChaseState(EnemyController enemy) => _enemy = enemy;

    public void OnEnter()
    {
        _enemy.agent.speed = _enemy.data.chaseSpeed;
        Debug.Log("Seni gördüm! Geliyorum!");
    }

    public void OnUpdate()
    {
        if (_enemy.player != null)
        {
            _enemy.agent.SetDestination(_enemy.player.position);
            _enemy.anim.SetFloat("Speed", _enemy.agent.velocity.magnitude);

            float distance = Vector3.Distance(_enemy.transform.position, _enemy.player.position);

           
            if (distance <= _enemy.data.attackRange)
            {
                _enemy.ChangeState(new AttackState(_enemy));
            }
        }
    }
    public void OnExit() { }
}