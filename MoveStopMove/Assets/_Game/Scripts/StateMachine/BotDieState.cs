using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BotDieState : IState<Bot>
{
    public void OnEnter(Bot bot)
    {
        bot.ChangeAnim(ConstString.ANIM_DEAD);
        bot.navMeshAgent.isStopped = true;
    }

    public void OnExecute(Bot bot)
    {
        bot.countDownDie -= Time.deltaTime;
        if (bot.countDownDie <= 0.1f)
        {
            bot.OnDespawn();
            bot.navMeshAgent.enabled = false;
        }
    }

    public void OnExit(Bot bot)
    {

    }
}
