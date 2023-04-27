using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{
    private bool isMove;
    private void Move(Vector3 direction)
    {
        // Rotation when move
        if (Vector3.Distance(direction, Vector3.zero) > 0.01f)
        {
            Quaternion rotation = Quaternion.LookRotation(direction, Vector3.up);
            isMove = true;
            rb.transform.rotation = rotation;
        }
        rb.velocity = direction.normalized * speed * Time.deltaTime;
    }
    public override void OnInit()
    {
        base.OnInit();
    }

    private void Start()
    {
        isMove = false;
    }
    // Update is called once per frame
    void Update()
    {
        if (joystick != null)
        {
            Move(Vector3.forward * joystick.Vertical + Vector3.right * joystick.Horizontal);
        }
        if (isMove)
        {
            ChangeAnim(ConstString.ANIM_RUN);
        }
        if(Vector3.Distance(Vector3.zero, rb.velocity) <= 0)
        {

            isMove = false;
            if (IsAttack)
            {
                ChangeAnim(ConstString.ANIM_ATTACK);
                transform.LookAt(currentTarget.transform.position);
                Timer += Time.deltaTime;
                if (Timer >= TimeToAttack)
                {
                    IsAttack = false;
                    Timer = 0;
                    ChangeAnim(ConstString.ANIM_IDLE);
                }
            }
            else
            {
                //IsAttack = false;
                ChangeAnim(ConstString.ANIM_IDLE);
            }
        }
    }
}
