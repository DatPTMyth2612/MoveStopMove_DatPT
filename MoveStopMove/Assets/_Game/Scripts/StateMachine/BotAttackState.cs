using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class BotAttackState : IState<Bot>
{
    public void OnEnter(Bot bot)
    {
        bot.Timer = 0;
        bot.ChangeAnim(ConstString.ANIM_ATTACK);
        bot.transform.LookAt(bot.currentTarget.transform.position);
    }

    public void OnExcute(Bot bot)
    {
        bot.Timer += Time.deltaTime;
        if (bot.Timer >= bot.TimeToAttack)
        {
            bot.Timer = 0;
            bot.ChangeState(bot.botIdleState);
        }
    }
    public void OnExit(Bot bot)
    { 

    }
}
