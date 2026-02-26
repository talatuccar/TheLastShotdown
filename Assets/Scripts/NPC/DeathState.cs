using AI.FSM;
using UnityEngine;

namespace AI.FSM
{
    public class DeathState : IState
    {
        private EnemyController _enemy;
        private DeathType _deathType; 

        
        public DeathState(EnemyController enemy, DeathType deathType = DeathType.General)
        {
            _enemy = enemy;
            _deathType = deathType;
        }

        public void OnEnter()
        {
            // 1. Hareket ve Zekayý durdur (Mevcut kodlarýn)
            if (_enemy.agent != null)
            {
                _enemy.agent.isStopped = true;
                _enemy.agent.enabled = false;
            }
            _enemy.StopAllCoroutines();

            // 2. Ölüm Tipine Göre Animasyon Seçimi
            //if (_enemy.anim != null)
            //{
            //    switch (_deathType)
            //    {
            //        case DeathType.Headshot:
            //            _enemy.anim.SetTrigger("Die_Headshot"); // Animator'daki parametre ismin
            //            break;


            //        default:
            //            _enemy.anim.SetTrigger("Die"); // Genel ölüm
            //            break;
            //    }
            //}
            if (_enemy.anim != null)
            {
                switch (_deathType)
                {
                    case DeathType.Headshot:
                        _enemy.anim.SetTrigger("Die_Headshot"); 
                        break;

                    default:
                        _enemy.anim.SetTrigger("Die"); 
                        break;
                }
            }

        }

        public void OnUpdate()
        {

        }

        public void OnExit()
        {

        }
    }
}