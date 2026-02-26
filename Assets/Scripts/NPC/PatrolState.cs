using UnityEngine;

public class PatrolState : IState
{
    private EnemyController _enemy;
    private int _currentWaypointIndex;

    public PatrolState(EnemyController enemy) => _enemy = enemy;

    public void OnEnter()
    {
      
        _enemy.agent.speed = _enemy.enemyData.patrolSpeed;
        if (_enemy.waypoints.Count > 0)
        {
            _currentWaypointIndex = Random.Range(0, _enemy.waypoints.Count);
        }
        SetDestination();
    }

    public void OnUpdate()
    {

       
       
        if (_enemy.Player != null)
        {
            _enemy.ChangeState(new ChaseState(_enemy));
            return;
        }

        if (!_enemy.agent.pathPending && _enemy.agent.remainingDistance < 0.5f)
        {
            _currentWaypointIndex = (_currentWaypointIndex + 1) % _enemy.waypoints.Count;
            SetDestination();
        }
    }

    private void SetDestination() 
    {
        int newIndex = _currentWaypointIndex;

        
        while (newIndex == _currentWaypointIndex)
        {
            newIndex = Random.Range(0, _enemy.waypoints.Count);
        }

        _currentWaypointIndex = newIndex;
        _enemy.agent.SetDestination(_enemy.waypoints[_currentWaypointIndex].transform.position);


    } 
    public void OnExit() { }
}