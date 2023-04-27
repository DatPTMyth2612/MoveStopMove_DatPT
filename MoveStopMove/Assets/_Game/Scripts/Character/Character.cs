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
    [SerializeField] internal CapsuleCollider capsuleCollider;
    [SerializeField] internal TargetRing targetRing;
    [SerializeField] internal Transform currentTarget;


    private string currentAnim;

    public List<Transform> TargetsInRange = new List<Transform>();
    public bool IsAttack;
    public bool IsDead;
    public float Timer = 0f;
    public float TimeToAttack = 0.5f;
    public float speed;


    private void Start()
    {
        OnInit();
    }
    public void Move()
    {

    }

    public void Attack()
    {

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
