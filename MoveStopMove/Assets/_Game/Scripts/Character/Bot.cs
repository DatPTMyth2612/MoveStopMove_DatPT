using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Bot : Character
{
    IState<Bot> currentState;
    public BotIdleState botIdleState = new BotIdleState();
    public BotMoveState botMoveState = new BotMoveState();
    public BotAttackState botAttackState = new BotAttackState();
    public NavMeshAgent navMeshAgent;
    
    

    // Start is called before the first frame update
    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        currentState = botIdleState;
        currentState.OnEnter(this);
    }

    // Update is called once per frame
    void Update()
    {
        currentState.OnExcute(this);
    }
    public void ChangeState(IState<Bot> newState)
    {
        currentState.OnExit(this);
        currentState = newState;
        currentState.OnEnter(this);
    }
}
