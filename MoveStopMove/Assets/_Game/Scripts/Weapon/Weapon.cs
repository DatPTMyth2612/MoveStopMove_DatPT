using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : GameUnit
{
    [SerializeField] internal Rigidbody rb;
    [SerializeField] internal Transform anim;

    internal Character owner;
    internal bool isHasFire;
    public Vector3 dirToTarget;
    public Vector3 startPos;


    public void SetDir(Vector3 dir)
    {
        dirToTarget = dir;
    }
    public virtual void Move()
    {
        if (Vector3.Distance(dirToTarget, Vector3.zero) > 0.01f)
        {
            Quaternion rotation = Quaternion.LookRotation(dirToTarget, Vector3.up);
            rb.transform.rotation = Quaternion.Euler(-90+rotation.x, rotation.y, rotation.z);
        }
        rb.velocity = dirToTarget * 4f;
        transform.up = -dirToTarget;
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
        Character character = Cache.GetCharacterInParent(other);
        if(other.gameObject.layer == 7)
        {
            OnDespawn();
            return;
        }
        if (other.CompareTag(ConstString.TAG_BOT))
        {
            if(character == owner)
            {
                return;
            }
            if(owner is Player)
            {
                owner.AddCoin();
            }
            owner.IncreaseExp(character.exp);
            owner.OnDeSelect();
            character.OnHit();
            OnDespawn();
        }
    }
    public override void OnDespawn()
    {
        SimplePool.Despawn(this);
    }

    public override void OnInit()
    {

    }
}
