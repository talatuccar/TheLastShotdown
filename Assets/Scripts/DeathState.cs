using AI.FSM;
using UnityEngine;

namespace AI.FSM
{
    public class DeathState : IState
    {
        private EnemyController _enemy;

        public DeathState(EnemyController enemy) => _enemy = enemy;

        public void OnEnter()
        {
            // --- PROFESYONEL ÖLÜM MANTIÐI ---

            // 1. NavMesh'i anýnda durdur (Ceset kaymasýn)
            if (_enemy.agent != null)
            {
                _enemy.agent.isStopped = true;
                _enemy.agent.enabled = false; // NavMesh collider'ý kapat ki mermiler içinden geçsin
            }

            // 2. Animasyonu tetikle (Animator'da "Die" Trigger'ý olmalý)
            if (_enemy.anim != null)
            {
                _enemy.anim.SetTrigger("Die");
            }

            // 3. NPC'nin beynini kapat (Tespit coroutine'ini durdur)
            _enemy.StopAllCoroutines();

            // 4. Collider'ý yönet (Ýstersen kapatabilirsin veya istersen açýk kalsýn)
            Collider col = _enemy.GetComponent<Collider>();
            if (col != null)
            {
                // col.enabled = false; // Cesedin üstünden geçilsin istersen bunu aç
            }

            Debug.Log("<color=red>Düþman Öldü: DeathState Aktif</color>");
        }

        public void OnUpdate()
        {
            // Ölü adam hareket etmez, Update boþ kalmalý.
        }

        public void OnExit()
        {
            // Öldükten sonra baþka duruma geçiþ yok.
        }
    }
}