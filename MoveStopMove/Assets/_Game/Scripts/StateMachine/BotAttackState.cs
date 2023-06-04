using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class BotAttackState : IState<Bot>
{
    public void OnEnter(Bot bot)
    {
        bot.ChangeAnim(ConstString.ANIM_ATTACK);
        if (bot.currentTarget != null)
        {
            if (bot.delayAttack <= 0.01f)
            {
                bot.RotationToTarget();
                bot.Attack();
            }
        }
    }

    public void OnExecute(Bot bot)
    {
        if (bot.IsAttackAnimEnd)
        {
            bot.ChangeState(bot.botIdleState);
        }
    }
    public void OnExit(Bot bot)
    { 

    }
}
