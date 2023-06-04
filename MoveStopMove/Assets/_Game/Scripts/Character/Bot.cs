using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Bot : Character
{
    internal Color currentColor;
    
    public int randomInt;
    public int randomInt2;

    IState<Bot> currentState;

    public static BotIdleState idle = new BotIdleState();
    public static BotIdleState move = new BotIdleState();
    public static BotIdleState attack = new BotIdleState();

    public BotIdleState botIdleState = new BotIdleState();
    public BotMoveState botMoveState = new BotMoveState();
    public BotAttackState botAttackState = new BotAttackState();
    public BotDieState botDieState = new BotDieState();
    public NavMeshAgent navMeshAgent;
    
    public override void OnInit()
    {
        base.OnInit();
        randomInt = Random.Range(0, 3);
        randomInt2 = Random.Range(0, 3);
        weaponBullet = WeaponConfig.Ins.weapon[randomInt].weapon;
        pant.GetComponent<SkinnedMeshRenderer>().material = SkinShopConfig.Ins.pant[randomInt2].pantMaterial;
        hairSkin = Instantiate(SkinShopConfig.Ins.hair[randomInt2].hair, hair);
        weaponOnHand = Instantiate(WeaponConfig.Ins.weapon[randomInt].weaponPrefab, hand);
        EquipWeapon(randomInt);
        currentState = botIdleState;
        currentState.OnEnter(this);
    }
    public override void OnDespawn()
    {
        base.OnDespawn();
    }
    private void Start()
    {
        //Time.timeScale = 0f;
    }
    // Update is called once per frame
    void Update()
    {
        if (isCoolDownAttack)
        {
            delayAttack -= Time.deltaTime;
        }
        if(GameManager.Ins.IsState(GameState.Gameplay) && currentState !=null)
        {
            currentState.OnExecute(this);
        }
    }
    public void ChangeState(IState<Bot> newState)
    {
        currentState.OnExit(this);
        currentState = newState;
        currentState.OnEnter(this);
    }
    public override void OnHit(Character attacker)
    {
        base.OnHit(attacker);
        wayPoint.OnDespawn();
        ChangeState(botDieState);
        ParticlePool.Play(LevelManager.Ins.bloodExplosion,new Vector3(TF.position.x, TF.position.y, TF.position.z), Quaternion.identity);
        ChangeAnim(ConstString.ANIM_DEAD);
        waitAfterDeathCoroutine = StartCoroutine( WaitAnimEnd(
                countDownDie,
                () =>
                {
                    ParticlePool.Play(LevelManager.Ins.deathParticle, new Vector3(TF.position.x, 2f, TF.position.z),Quaternion.Euler(0f, 180f, 0f));
                    StopCoroutine(waitAfterDeathCoroutine);
                    OnDespawn();
                }
            )
        );
        currentStage.characterColorAvaible.Add(currentColor);
        currentStage.OnCharacterDie(this);
    }
    public void ChangeColorBody(Color newColor)
    {
        currentColor = newColor;
        bodyRenderer.material.SetColor("_Color", newColor);
    }
}
