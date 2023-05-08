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
    internal NavMeshAgent navMeshAgent;

    public override void OnInit()
    {
        base.OnInit();
        navMeshAgent= GetComponent<NavMeshAgent>();
    }

    // Start is called before the first frame update
    void Start()
    {
        OnInit();
        currentState = botIdleState;
        currentState.OnEnter(this);
    }

    // Update is called once per frame
    void Update()
    {
        if (isCoolDownAttack)
        {
            delayAttack -= Time.deltaTime;
        }
        currentState.OnExcute(this);
        if(IsDead)
        {
            ChangeAnim(ConstString.ANIM_DEAD);
            navMeshAgent.SetDestination(TF.position);   
            countDownDie -=Time.deltaTime;
            if(countDownDie <= 0.1f)
            {
                OnDespawn();
                navMeshAgent.enabled = false;
            }
        }
        
    }
    public void ChangeState(IState<Bot> newState)
    {
        currentState.OnExit(this);
        currentState = newState;
        currentState.OnEnter(this);
    }
}
