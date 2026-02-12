public class PatrolState : IState
{
    private EnemyController _enemy;
    private int _currentWaypointIndex;

    public PatrolState(EnemyController enemy) => _enemy = enemy;

    public void OnEnter()
    {
        // Hata buradaydý: Veri artýk data.patrolSpeed içinde
        _enemy.agent.speed = _enemy.data.patrolSpeed;
        SetDestination();
    }

    public void OnUpdate()
    {

        _enemy.anim.SetFloat("Speed", _enemy.agent.velocity.magnitude);
        // Oyuncu menzile girdiyse Takip durumuna geç
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