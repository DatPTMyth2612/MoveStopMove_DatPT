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
    [SerializeField] internal CharacterColider characterColider;
    [SerializeField] internal TargetRing targetRing;
    [SerializeField] internal Transform currentTarget;
    [SerializeField] internal Transform spawnPoint;


    private string currentAnim;
    internal Coroutine waitAfterAtkCoroutine;
    internal bool isCoolDownAttack = false;


    public Weapon w;
    public List<Transform> TargetsInRange = new List<Transform>();
    public bool IsFire;
    public bool isAttackAnimEnd = false;
    public bool IsDead;
    public float delayAttack = 0f;
    public float speed;
    public delegate void CallbackMethod();
    internal float countDownDie = 1.5f;



    private void Start()
    {
        OnInit();
    }
    private void Update()
    {
        
    }
    public void Move()
    {

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
    }
    public void StartCoroutineAttack()
    {
        //ChangeAnim(ConstString.ANIM_ATTACK);

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
        Weapon weaponBulletUnit = SimplePool.Spawn<Weapon>(w,spawnPoint.position, rotation);
        weaponBulletUnit.SetDir(dir);
        weaponBulletUnit.isHasFire = true;
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
    }
    public override void OnDespawn()
    {
        SimplePool.Despawn(this);
    }
    public void OnHit()
    {
        IsDead = true;
        //ChangeAnim(ConstString.ANIM_DEAD);
        //OnDespawn();
    }
    public void OnSelect()
    {
        targetRing.EnableRing();
    }
    public void OnDeSelect()
    {
        targetRing.DisableRing();
    }
}
