using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{
    public int currentWeaponIndex;
    private void Move(Vector3 direction)
    {
        // Rotation when move
        if (Vector3.Distance(direction, Vector3.zero) > 0.01f)
        {
            Quaternion rotation = Quaternion.LookRotation(direction, Vector3.up);
            rb.transform.rotation = rotation;
        }
        rb.velocity = direction.normalized * speed;
    }

    public override void OnInit()
    {
        base.OnInit();
        
    }

    private void Start()
    {
        currentWeaponIndex = PlayerPrefs.GetInt("Selected Weapon", 0);
        weaponBullet = WeaponConfig.Ins.weapon[currentWeaponIndex].weapon;
        onHand = Instantiate(WeaponConfig.Ins.weapon[currentWeaponIndex].weaponPrefab, hand);
        pant.GetComponent<SkinnedMeshRenderer>().material = PantsConfig.Ins.pant[0].pantMaterial;
    }
    // Update is called once per frame
    void Update()
    {
        if (joystick != null)
        {
            Move(Vector3.forward * joystick.Vertical + Vector3.right * joystick.Horizontal);
        }
        if (isCoolDownAttack)
        {
            delayAttack -= Time.deltaTime;
        }
        if (Vector3.Distance(Vector3.zero, rb.velocity) <= 0)
        {
            if (currentTarget != null)
            {
                if (IsFire)
                {
                    IsFire = false;
                    if (delayAttack <= 0)
                    {
                        RotationToTarget();
                        Attack();
                    }
                }
                if(isAttackAnimEnd)
                {
                    ChangeAnim(ConstString.ANIM_IDLE);
                }
            }
            else
                ChangeAnim(ConstString.ANIM_IDLE);
        }
        else
        {
            ChangeAnim(ConstString.ANIM_RUN);
        }
        //if (IsDead)
        //{
        //    ChangeAnim(ConstString.ANIM_DEAD);
        //    countDownDie -= Time.deltaTime;
        //    if (countDownDie <= 0.1f)
        //    {
        //        OnDespawn();
        //    }
        //}
    }
}
