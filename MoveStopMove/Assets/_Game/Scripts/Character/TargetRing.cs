using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetRing : MonoBehaviour
{
    public SpriteRenderer spriteRing;

    public void OnInit()
    {
        spriteRing= GetComponent<SpriteRenderer>();
        DisableRing();
    }
    public void EnableRing()
    {
        spriteRing.enabled = true;
    }
    public void DisableRing()
    {
        spriteRing.enabled = false;
    }
}
