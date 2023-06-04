using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class Character : GameUnit
{
    [SerializeField] internal FloatingJoystick joystick;
    [SerializeField] internal Rigidbody rb;
    [SerializeField] internal Animator anim;
    [SerializeField] internal AttackRange attackRange;
    [SerializeField] internal CharacterCollider characterCollider;
    [SerializeField] internal TargetRing targetRing;
    [SerializeField] internal Transform currentTarget;
    [SerializeField] internal Transform spawnPoint;
    [SerializeField] internal Weapon weaponBullet;
    [SerializeField] internal GameObject weaponOnHand;
    [SerializeField] internal Transform hand;
    [SerializeField] internal Transform hair;
    [SerializeField] internal GameObject pant;
    [SerializeField] internal Stage currentStage;
    [SerializeField] internal SkinnedMeshRenderer bodyRenderer;


    private string currentAnim;
    internal Vector3 attackRangeDefault;
    internal Coroutine waitAfterAtkCoroutine;
    internal bool isCoolDownAttack = false;
    internal GameObject hairSkin;
    internal Coroutine waitAfterDeathCoroutine;
    internal MissionWaypoint wayPoint;
    

    public List<Transform> m_TargetsInRange = new List<Transform>();
    public bool IsFire;
    public bool IsBoost;
    protected bool isAttackAnimEnd = false;
    public bool IsAttackAnimEnd => isAttackAnimEnd;

    public bool IsDead;
    public float delayAttack = 0f;
    public float speed;
    public delegate void CallbackMethod();
    internal float countDownDie = 1.5f;
    internal float exp;



    private void Start()
    {
        OnInit();
    }


    public void Attack()
    {
        isAttackAnimEnd = false;
        if (delayAttack >= 0.01f)
        {
            return;
        }
        isCoolDownAttack = true;
        delayAttack = 2f;
        Vector3 dir = GetDirToFireWeapon();
        ChangeAnim(ConstString.ANIM_ATTACK);
        SpawnWeaponBullet(dir);
        StartCoroutineAttack();
        if (!IsBoost) return;         
        OnResetPropertie();
    }
    public void EquipWeapon(int weaponIndex)
    {
        weaponBullet = WeaponConfig.Ins.weapon[weaponIndex].weapon;
        weaponBullet.speed = WeaponConfig.Ins.weapon[weaponIndex].weaponSpeed;
        float extraRange = WeaponConfig.Ins.weapon[weaponIndex].weaponExtraRange;
        attackRange.gameObject.transform.localScale += new Vector3(extraRange, extraRange, extraRange);
        CharacterPropertie.Ins.currentAttackRangeScale = attackRange.gameObject.transform.localScale.x;
    }
    public void StartCoroutineAttack()
    {

        float animLength = anim.GetCurrentAnimatorStateInfo(0).length;

        waitAfterAtkCoroutine = StartCoroutine(
            WaitAnimEnd(
                animLength,
                () =>
                {
                    StopCoroutine(waitAfterAtkCoroutine);
                    isAttackAnimEnd = true;
                }
            )
        );
    }
    public IEnumerator WaitAnimEnd(float animLength, CallbackMethod cb)
    {
        yield return new WaitForSeconds(animLength);
        if (cb != null)
        {
            cb();
        }
    }
    public Vector3 GetDirToTarget()
    {
        Transform targetColliderTF = currentTarget;
        return (
            new Vector3(targetColliderTF.position.x, TF.position.y, targetColliderTF.position.z)
            - TF.position
        ).normalized;
    }
    public Vector3 GetDirToFireWeapon()
    {
        Transform targetColliderTF = currentTarget;
        return (targetColliderTF.position - TF.position).normalized;
    }
    public void SpawnWeaponBullet(Vector3 dir)
    {
        Quaternion rotation = Quaternion.LookRotation(dir, Vector3.up);
        Weapon weaponBulletUnit = SimplePool.Spawn<Weapon>(weaponBullet,TF.position, rotation);
        weaponBulletUnit.OnInit(this);
        weaponBulletUnit.SetDir(dir);
    }

    public void RotationToTarget()
    {
        if (currentTarget != null)
        {
            Vector3 direction = GetDirToTarget();
            Quaternion rotation = Quaternion.LookRotation(direction, Vector3.up);
            rb.transform.rotation = rotation;
        }
    }
    public void ChangeAnim(string animName)
    {
        if (currentAnim != animName)
        {
            anim.ResetTrigger(currentAnim);
            currentAnim = animName;
            anim.SetTrigger(currentAnim);
        }
    }
   
    public override void OnInit()
    {
        joystick = GameManager.Ins.joystick;
        targetRing.OnInit();
        IsDead = false;
        TF.localScale = Vector3.one;
        CharacterPropertie.Ins.currentScale = TF.localScale.x;
        exp = 1;
    }
    public override void OnDespawn()
    {
        SimplePool.Despawn(this);
        m_TargetsInRange.Clear();
    }
    public virtual void OnHit(Character attacker)
    {
        IsDead = true;
    }
    public void OnSelect()
    {
        targetRing.EnableRing();
    }
    public void OnDeSelect()
    {
        IsFire = false;
        m_TargetsInRange.Remove(currentTarget);
        targetRing.DisableRing();
    }
    public void IncreaseExp(float expEnemy)
    {
        exp += expEnemy;
        TF.localScale += new Vector3(expEnemy * 0.05f, expEnemy * 0.05f, expEnemy * 0.05f);
        CharacterPropertie.Ins.currentScale = TF.localScale.x;
        CharacterPropertie.Ins.currentAttackRangeScale = attackRange.gameObject.transform.localScale.x;
        //CurrentScale();
    }
    public void OnBoost()
    {
        IsBoost = true;
        TF.localScale = Vector3.one * 1.5f;
        attackRange.gameObject.transform.localScale *= 1.5f;
    }
    public void OnResetPropertie()
    {
        IsBoost = false;
        TF.localScale = CharacterPropertie.Ins.currentScale * Vector3.one;
        attackRange.gameObject.transform.localScale = CharacterPropertie.Ins.currentAttackRangeScale * Vector3.one;
    }
    public void AddCoin()
    {
        LevelManager.Ins.coin += 50;
        LevelManager.Ins.SetCoinText();
    }
}
