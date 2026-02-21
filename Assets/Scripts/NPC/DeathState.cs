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
           
            if (_enemy.agent != null)
            {
                _enemy.agent.isStopped = true;
                _enemy.agent.enabled = false; 
            }

           
            if (_enemy.anim != null)
            {
                _enemy.anim.SetTrigger("Die");
            }

            // 3. NPC'nin beynini kapat (Tespit coroutine'ini durdur)
            _enemy.StopAllCoroutines();
            
           
        }

        public void OnUpdate()
        {
            
        }

        public void OnExit()
        {
            
        }
    }
}