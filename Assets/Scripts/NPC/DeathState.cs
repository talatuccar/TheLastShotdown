using AI.FSM;
using System.Collections;
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
            int hitLayerIndex = _enemy.anim.GetLayerIndex("HitLayer");
            if (hitLayerIndex != -1)
            {
                _enemy.anim.SetLayerWeight(hitLayerIndex, 0f);
            }
            _enemy.anim.ResetTrigger("Hit_Idle");
            _enemy.anim.ResetTrigger("Hit_Running");
            
            if (_enemy.agent != null && _enemy.agent.isActiveAndEnabled)
            {
                _enemy.agent.isStopped = true;
            }

           
            if (_enemy.anim != null)
            {
               
                switch (_deathType)
                {
                    case DeathType.Headshot:
                        _enemy.anim.SetTrigger("Die_Headshot");
                        break;
                    case DeathType.ChaseDeath:
                        _enemy.anim.SetFloat("Speed", 5f);
                        _enemy.anim.SetTrigger("Die_Running");
                        break;
                    default:
                        _enemy.anim.SetTrigger("Die");
                        break;
                }
            }

            
            // çok kýsa bir süre sonraya ertelemek en saðlýklýsýdýr.
            _enemy.StartCoroutine(DisableAgentDelayed());
        }

        private IEnumerator DisableAgentDelayed()
        {
            yield return new WaitForSeconds(0.1f);
            if (_enemy.agent != null) _enemy.agent.enabled = false;
        }
        public void OnUpdate()
        {

        }

        public void OnExit()
        {

        }
    }
}