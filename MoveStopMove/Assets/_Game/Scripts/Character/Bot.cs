using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Bot : Character
{
    internal MissionWaypoint wayPoint;
    public int randomInt;
    public int randomInt2;

    IState<Bot> currentState;
    public BotIdleState botIdleState = new BotIdleState();
    public BotMoveState botMoveState = new BotMoveState();
    public BotAttackState botAttackState = new BotAttackState();
    public BotDieState botDieState = new BotDieState();
    internal NavMeshAgent navMeshAgent;

    public override void OnInit()
    {
        base.OnInit();
        navMeshAgent= GetComponent<NavMeshAgent>();
    }
    private void Start()
    {
        randomInt = Random.Range(0, 3);
        randomInt2 = Random.Range(0, 3);
        weaponBullet = WeaponConfig.Ins.weapon[randomInt].weapon;
        pant.GetComponent<SkinnedMeshRenderer>().material = SkinShopConfig.Ins.pant[randomInt2].pantMaterial;
        hairSkin = Instantiate(SkinShopConfig.Ins.hair[randomInt2].hair, hair);
        weaponOnHand = Instantiate(WeaponConfig.Ins.weapon[randomInt].weaponPrefab, hand);
        currentState = botIdleState;
        currentState.OnEnter(this);
        //Time.timeScale = 0f;
    }
    // Update is called once per frame
    void Update()
    {
        if (isCoolDownAttack)
        {
            delayAttack -= Time.deltaTime;
        }
        currentState.OnExecute(this);
    }
    public void ChangeState(IState<Bot> newState)
    {
        currentState.OnExit(this);
        currentState = newState;
        currentState.OnEnter(this);
    }
    public override void OnHit()
    {
        base.OnHit();
        wayPoint.OnDespawn();
        ChangeState(botDieState);
        curretStage.OnCharacterDie(this);
        //LevelManager.Ins.RespawnEnemy();
    }
}
