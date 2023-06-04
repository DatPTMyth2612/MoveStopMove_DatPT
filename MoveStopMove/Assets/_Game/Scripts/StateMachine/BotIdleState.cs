using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotIdleState : IState<Bot>
{
    private float changeStateTimer;
    private float timeEnd;
    public void OnEnter(Bot bot)
    {
        changeStateTimer = 0.0f;
        timeEnd = Random.Range(0.1f, 3.0f);
        bot.ChangeAnim(ConstString.ANIM_IDLE);
    }

    public void OnExecute(Bot bot)
    {
        changeStateTimer += Time.deltaTime;
        if (changeStateTimer >= timeEnd)
        {
            bot.ChangeState(bot.botMoveState);
        }
        if (bot.IsFire)
        {
            bot.ChangeState(bot.botAttackState);
           // bot.navMeshAgent.SetDestination(bot.TF.position);
        }
    }
    public void OnExit(Bot bot)
    {

    }
}
