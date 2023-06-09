using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : GameUnit
{
    [SerializeField] internal Rigidbody rb;
    [SerializeField] internal Transform anim;

    internal Character owner;
    internal bool isHasFire;
    internal float speedRotation = 400f;
    internal float speed;
    public Vector3 dirToTarget;
    public Vector3 startPos;
    public float maxDistance;

    public void SetDir(Vector3 dir)
    {
        dirToTarget = dir;
        transform.forward = dirToTarget;    
    }
    public virtual void Move()
    {
        rb.velocity = dirToTarget * 4f * speed;
        if(Vector3.Distance(transform.position, startPos) >= maxDistance&&!owner.IsBoost) 
        {
            OnDespawn();
        }
    }
    private void Update()
    {
        if (isHasFire && anim != null)
        {
            Move();
        }

    }
    private void OnTriggerEnter(Collider other)
    {
        CharacterCollider character = Cache.GetCharacterInParent(other);
        if(other.gameObject.layer == 7)
        {
            OnDespawn();
            return;
        }
        if (other.CompareTag(ConstString.TAG_BOT))
        {
            if(character == owner.characterCollider)
            {
                return;
            }
            if(owner is Player)
            {
                owner.AddCoin();
            }
            if(!character.character.IsDead) 
            {
                owner.IncreaseExp(character.character.exp);
            }
            owner.OnDeSelect();
            character.character.OnHit(owner);
            OnDespawn();
        }

        
        //IHit hit = other.GetComponent<IHit>();
        //if (hit != null)
        //{
        //    hit.OnHit();
        //}
    }
    public override void OnDespawn()
    {
        SimplePool.Despawn(this);
    }

    public override void OnInit()
    {
        
    }
    public void OnInit(Character owner)
    {
        this.owner = owner;
        TF.localScale = owner.TF.localScale;
        isHasFire = true;
        speed = owner.weaponBullet.speed;
        startPos = owner.TF.position;
        maxDistance = owner.attackRange.GetAttackRadius();
        if (!owner.IsBoost) return;
        speed *= 2;
    }
}
