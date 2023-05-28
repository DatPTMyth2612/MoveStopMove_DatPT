using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hammer : Weapon
{
    public override void Move()
    {
        base.Move();
        rb.AddTorque(dirToTarget * 5f);
    }
}
