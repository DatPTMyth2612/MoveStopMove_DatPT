using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Bot : Character
{
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
        //randomInt = Random.Range(0, 3);
        //randomInt2 = Random.Range(0, 3);
        //weaponBullet = WeaponConfig.Ins.weapon[randomInt].weapon;
        //pant.GetComponent<SkinnedMeshRenderer>().material = PantsConfig.Ins.pant[randomInt2].pantMaterial;
        //onHand = Instantiate(WeaponConfig.Ins.weapon[randomInt].weaponPrefab, hand);
    }

    // Start is called before the first frame update
    void Start()
    {
        OnInit();
        randomInt = Random.Range(0, 3);
        randomInt2 = Random.Range(0, 3);
        weaponBullet = WeaponConfig.Ins.weapon[randomInt].weapon;
        pant.GetComponent<SkinnedMeshRenderer>().material = PantsConfig.Ins.pant[randomInt2].pantMaterial;
        onHand = Instantiate(WeaponConfig.Ins.weapon[randomInt].weaponPrefab, hand);
        currentState = botIdleState;
        currentState.OnEnter(this);
        Time.timeScale = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (isCoolDownAttack)
        {
            delayAttack -= Time.deltaTime;
        }
        currentState.OnExecute(this);
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
    public override void OnHit()
    {
        ChangeState(botDieState); 
    }
}
