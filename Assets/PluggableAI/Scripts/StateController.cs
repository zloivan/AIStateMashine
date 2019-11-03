using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Complete;
using PluggableAI.Scripts;

public class StateController : MonoBehaviour
{
    public EnemyStats enemyStats;
    public Transform eyes;


    public State CurrentState
    {
        get => currentState;
        set => currentState = value;
    }

    public NavMeshAgent NavMeshAgent { get; set; }
    public TankShooting Shooting { get; set; }
    public List<Transform> WayPointList { get; set; }
    public int NextWayPoint { get; set; }

    private bool aiActive;
    [SerializeField] private State currentState;


    private void Awake()
    {
        Shooting = GetComponent<TankShooting>();
        NavMeshAgent = GetComponent<NavMeshAgent>();
    }

    public void SetupAI(bool aiActivationFromTankManager, List<Transform> wayPointsFromTankManager)
    {
        WayPointList = wayPointsFromTankManager;
        aiActive = aiActivationFromTankManager;
        if (aiActive)
        {
            NavMeshAgent.enabled = true;
        }
        else
        {
            NavMeshAgent.enabled = false;
        }
    }

    private void Update()
    {
        if (aiActive == false)
        {
            return;
        }

        CurrentState.UpdateState(this);
    }

    private void OnDrawGizmos()
    {
        if (CurrentState != null && eyes != null)
        {
            Gizmos.color = CurrentState.SceneGizmoColor;
            Gizmos.DrawWireSphere(eyes.position, enemyStats.lookSphereCastRadius);
        }
    }
}