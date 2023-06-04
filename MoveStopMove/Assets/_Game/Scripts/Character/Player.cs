using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
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
        attackRangeDefault = attackRange.gameObject.transform.localScale;
        currentWeaponIndex = PlayerPrefs.GetInt("Selected Weapon", 0);
        CreateWeapon(currentWeaponIndex);
        EquipWeapon(currentWeaponIndex);
        pant.GetComponent<SkinnedMeshRenderer>().material = SkinShopConfig.Ins.pant[0].pantMaterial;
        hairSkin = Instantiate(SkinShopConfig.Ins.hair[2].hair, hair);
    }
    public void CreateWeapon(int weaponIndex)
    {
        if (weaponOnHand != null)
        {
            Destroy(weaponOnHand);
            weaponOnHand = Instantiate(WeaponConfig.Ins.weapon[weaponIndex].weaponPrefab, hand);
        }
        else
        {
            weaponOnHand = Instantiate(WeaponConfig.Ins.weapon[weaponIndex].weaponPrefab, hand);
        }
    }
    
    void Update()
    {
        if (GameManager.Ins.IsState(GameState.Gameplay) && joystick != null)
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
                    //IsFire = false;
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
        if (IsDead)
        {
            ChangeAnim(ConstString.ANIM_DEAD);
            rb.velocity = Vector3.zero;
        }
    }
    public override void OnHit(Character attacker)
    {
        base.OnHit(attacker);
        wayPoint.OnDespawn();
        ParticlePool.Play(LevelManager.Ins.bloodExplosion, new Vector3(TF.position.x, TF.position.y, TF.position.z), Quaternion.identity);
        currentStage.OnCharacterDie(this);
        waitAfterDeathCoroutine = StartCoroutine(WaitAnimEnd(
                countDownDie,
                () =>
                {
                    ParticlePool.Play(LevelManager.Ins.deathParticle, new Vector3(TF.position.x, 2f, TF.position.z), Quaternion.Euler(0f, 180f, 0f));
                    StopCoroutine(waitAfterDeathCoroutine);
                    OnDespawn();
                }
            )
        );
        UIManager.Ins.CloseUI<Gameplay>();
        UIManager.Ins.OpenUI<Lose>().SetText(attacker);
        GameManager.Ins.ChangeState(GameState.Pause);
        GameManager.Ins.inGame.gameObject.SetActive(false);
    }
}
