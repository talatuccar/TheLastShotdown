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

        //public void OnEnter()
        //{

        //    if (_enemy.agent != null)
        //    {
        //        _enemy.agent.isStopped = true;
        //        _enemy.agent.enabled = false;
        //    }
        //    _enemy.StopAllCoroutines();


        //    if (_enemy.anim != null)
        //    {
        //        switch (_deathType)
        //        {
        //            case DeathType.Headshot:
        //                _enemy.anim.SetTrigger("Die_Headshot");
        //                break;
        //            case DeathType.ChaseDeath:
        //                _enemy.anim.SetTrigger("Die_Running");
        //                break;
        //            default:
        //                _enemy.anim.SetTrigger("Die");
        //                break;
        //        }
        //    }

        //}
        public void OnEnter()
        {
            int hitLayerIndex = _enemy.anim.GetLayerIndex("HitLayer");
            if (hitLayerIndex != -1)
            {
                _enemy.anim.SetLayerWeight(hitLayerIndex, 0f);
            }
            _enemy.anim.ResetTrigger("Hit_Idle");
            _enemy.anim.ResetTrigger("Hit_Running");
            // 1. Önce NavMesh'i durdur (Ama objeyi kapatma!)
            if (_enemy.agent != null && _enemy.agent.isActiveAndEnabled)
            {
                _enemy.agent.isStopped = true;
            }

            // 2. Animasyonu tetikle
            if (_enemy.anim != null)
            {
                // Burada Animator'ýn o anki hýzý sýfýrlamasýný engellemiþ olduk
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

            // 3. EN ÖNEMLÝ KISIM: 
            // Agent.enabled = false iþlemini animasyon baþladýktan 
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