using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using AI.FSM;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    [Header("Settings")]
    public EnemyDataSo data;
    public List<Transform> waypoints;
    public LayerMask playerLayer;

    [Header("Components")]
    public NavMeshAgent agent;
    public Animator anim;

    private StateMachineHandler _stateMachine;
    public Transform player;

    void Start()
    {
        _stateMachine = new StateMachineHandler();
        _stateMachine.AddState(new PatrolState(this));
        StartCoroutine(DetectionRoutine());
    }

    void Update()
    {
        _stateMachine.UpdateStates();
        anim.SetFloat("Speed", agent.velocity.magnitude);
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
       
        if (data == null) return;

        Collider[] hits = Physics.OverlapSphere(transform.position, data.detectionRadius, playerLayer);
        if (hits.Length > 0)
        {
            player = hits[0].transform;
        }
        else
        {
            player = null;
        }
    }
}