using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using AI.FSM;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    [Header("Settings")]
    public EnemyDataSo enemyData;
    public List<Transform> waypoints;
    public LayerMask playerLayer;

    [Header("Components")]
    public NavMeshAgent agent;
    public Animator anim;

    private StateMachineHandler _stateMachine;
    public Transform player;
    public Transform muzzlePoint;
    public  ParticleSystem muzzleFlashParticle;

    public GameObject flashGo;

    [Header("Password Settings")]
    public bool carriesPasswordPart; 
    public int passwordIndex;       
    void Start()
    {
         flashGo = Instantiate(enemyData.muzzleFlashPrefab, muzzlePoint.position, muzzlePoint.rotation, muzzlePoint);


        muzzleFlashParticle = flashGo.GetComponent<ParticleSystem>();
        _stateMachine = new StateMachineHandler();
        _stateMachine.AddState(new PatrolState(this));
        StartCoroutine(DetectionRoutine());
    }

    void Update()
    {
        _stateMachine.UpdateStates();

        
        float currentSpeed = agent.velocity.magnitude;
        anim.SetFloat("Speed", currentSpeed > 0.1f ? currentSpeed : 0f);
    }

    public void ChangeState(IState newState)
    {
        _stateMachine.AddState(newState);
    }

    
    public void GoBackToPreviousState()
    {
        _stateMachine.RemoveState();
    }

    IEnumerator DetectionRoutine()
    {
        while (true)
        {
            DetectPlayer();
            yield return new WaitForSeconds(0.2f);
        }
    }

    private void DetectPlayer()
    {
       
        if (enemyData == null) return;

        Collider[] hits = Physics.OverlapSphere(transform.position, enemyData.detectionRadius, playerLayer);
        if (hits.Length > 0)
        {
            player = hits[0].transform;
        }
        else
        {
            player = null;
        }
    }

   
    public bool CanSeePlayer()
    {
        if (player == null) return false;

        Vector3 directionToPlayer = (player.position - transform.position).normalized;
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        
        Vector3 eyePosition = transform.position + Vector3.up * 1.5f;

        RaycastHit hit;
        
        if (Physics.Raycast(eyePosition, directionToPlayer, out hit, enemyData.detectionRadius))
        {
            
            if (hit.transform.CompareTag("Player"))
            {
                return true;
            }
        }
        return false;
    }

    
    public void ShowPasswordDigit()
    {

        Debug.Log("EVENT ÇALIÞTI!");
        if (carriesPasswordPart)
        {
            int digit = GameManager.Instance.passwordManager.GetPasswordPart(passwordIndex);

            
            GameManager.Instance.passwordManagerUI.ShowPasswordFragment(passwordIndex + 1, digit);
        }
    }
}