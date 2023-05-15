using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BotMoveState : IState<Bot>
{
    public void OnEnter(Bot bot)
    {
        bot.navMeshAgent.SetDestination(GetRandomPosition(bot));
        bot.ChangeAnim(ConstString.ANIM_RUN);
    }

    public void OnExecute(Bot bot)
    {
        if (bot.IsFire)
        {
            bot.navMeshAgent.SetDestination(bot.TF.position);
            bot.ChangeState(bot.botAttackState);
        }
        if (bot.navMeshAgent.remainingDistance <= 0.1f)
        {
            bot.ChangeState(bot.botIdleState);
        }
    }

    public void OnExit(Bot bot)
    {

    }
    private Vector3 GetRandomPosition(Bot bot)
    {
        float radius = 15f;
        Vector2 randomPoint = Random.insideUnitCircle.normalized;
        Vector3 randomPosition = new Vector3(randomPoint.x, 0f, randomPoint.y) * radius;
        randomPosition += bot.TF.position;
        return randomPosition;
    }
}
