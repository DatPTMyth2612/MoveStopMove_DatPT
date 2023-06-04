using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Axe : Weapon
{
    public override void Move()
    {
        base.Move();
        transform.eulerAngles += Vector3.up * speedRotation * Time.deltaTime;
    }
}
