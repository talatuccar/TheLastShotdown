using AI.FSM;
using UnityEngine;

public class CoverState : IState
{
    private EnemyController _enemy;
    private float _checkInterval = 0.2f; // Saniyede 5 kez kontrol yeterli
    private float _timer;

    public CoverState(EnemyController enemy) => _enemy = enemy;

    public void OnEnter() { /* Entry zaten Animator'da baðlý, boþ kalabilir */ }

    public void OnUpdate()
    {
        // 1. Oyuncu yoksa hiçbir þey yapma
        if (_enemy.Player == null) return;

        _timer += Time.deltaTime;
        if (_timer >= _checkInterval)
        {
            _timer = 0;

            float dist = Vector3.Distance(_enemy.transform.position, _enemy.Player.position);

            // 2. Eðer oyuncu "Görüþ Menzili" içindeyse (Örn: 10 metre)
            if (dist < _enemy.enemyData.detectionRadius)
            {
                // 3. Çok yakýna gelirse oyuncuya DÖN ve ATEÞ ET
                // (Görünmeyen noktadan gelse bile dibine girince fark etmiþ olur)
                Vector3 dir = (_enemy.Player.position - _enemy.transform.position).normalized;
                dir.y = 0; // Yerden yukarý bakmasýn, sadece dönsün
                _enemy.transform.rotation = Quaternion.Slerp(_enemy.transform.rotation, Quaternion.LookRotation(dir), Time.deltaTime * 10f);

                // Ateþ etme animasyonunu tetikle
                _enemy.anim.SetTrigger("PeekAndShoot");
            }
            else
            {
                // 4. Oyuncu menzilden çýkarsa siperde bekleme animasyonuna dön
                _enemy.anim.SetTrigger("Hide_In_Cover");
            }
        }
    }

    public void OnExit() { }
}