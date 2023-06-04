using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hammer : Weapon
{
    public override void Move()
    {
        base.Move();
        transform.eulerAngles += Vector3.up * speedRotation * Time.deltaTime;
    }
}
