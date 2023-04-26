using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Character : MonoBehaviour
{
    public Animator anim;
    private string currentAnim;
    public bool IsAttack;
    public float AttackInterval;
    private void OnInit()
    {

    }
    public void Move()
    {

    }
    public void Attack() 
    {
        
    }
    public void ChangeAnim(string animName)
    {
        if (currentAnim != animName)
        {
            anim.ResetTrigger(currentAnim);
            currentAnim = animName;
            anim.SetTrigger(currentAnim);
        }
    }
}
