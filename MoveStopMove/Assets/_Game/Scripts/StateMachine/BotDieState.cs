using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BotDieState : IState<Bot>
{
    public void OnEnter(Bot bot)
    {
        bot.navMeshAgent.isStopped = true;
    }

    public void OnExecute(Bot bot)
    {
       
    }

    public void OnExit(Bot bot)
    {

    }
}
