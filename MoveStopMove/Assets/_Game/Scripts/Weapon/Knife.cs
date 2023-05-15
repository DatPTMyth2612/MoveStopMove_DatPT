using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knife : Weapon
{
    public override void Move()
    {
        base.Move();
        transform.up = -dirToTarget;
    }
}
