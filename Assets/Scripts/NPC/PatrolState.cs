public class PatrolState : IState
{
    private EnemyController _enemy;
    private int _currentWaypointIndex;

    public PatrolState(EnemyController enemy) => _enemy = enemy;

    public void OnEnter()
    {
      
        _enemy.agent.speed = _enemy.enemyData.patrolSpeed; 
        SetDestination();
    }

    public void OnUpdate()
    {

       
       
        if (_enemy.player != null)
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

    private void SetDestination() => _enemy.agent.SetDestination(_enemy.waypoints[_currentWaypointIndex].position);
    public void OnExit() { }
}