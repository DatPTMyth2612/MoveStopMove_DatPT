using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{
    public CharacterController controller;
    [SerializeField] private float speed = 5;
    private FloatingJoystick joystick;
    private Vector3 direction;
    private float gravity = -10f;


    private void Start()
    {
        joystick = FindObjectOfType<FloatingJoystick>();
    }
    // Update is called once per frame
    void Update()
    {
        direction = new Vector3(joystick.Direction.x, 0f, joystick.Direction.y).normalized;
        if (Vector3.Distance(Vector3.zero, direction) > 0.1f)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0f, targetAngle, 0f);
            controller.Move(direction * speed * Time.deltaTime);
            ChangeAnim("run");
        }
        else
        {
            if (IsAttack)
            {
                ChangeAnim("attack");
                AttackInterval -= Time.deltaTime;
                if(AttackInterval<=0)
                {
                    IsAttack = false;
                    ChangeAnim("idle");
                }
            }
            else
            {
                ChangeAnim("idle");
            }
        }
    }
}
