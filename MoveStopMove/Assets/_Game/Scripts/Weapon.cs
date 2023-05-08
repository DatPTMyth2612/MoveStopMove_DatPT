using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : GameUnit
{
    [SerializeField] internal Rigidbody rb;
    [SerializeField] internal Transform anim;

    internal bool isHasFire;
    private Vector3 dirToTarget;
    private Vector3 startPos;


    public void SetDir(Vector3 dir)
    {
        dirToTarget = dir;
    }
    public virtual void Move()
    {
        rb.velocity = dirToTarget * 5f;
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
        if(other.gameObject.layer == 7)
        {
            OnDespawn();
            return;
        }
        if (other.CompareTag(ConstString.TAG_BOT))
        {
            OnDespawn();
            other.GetComponentInParent<Character>().OnHit();
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
