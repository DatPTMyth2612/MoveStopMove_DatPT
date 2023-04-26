using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackRange : MonoBehaviour
{
    [SerializeField] private Character player;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Bot"))
        {
           player.IsAttack = true;
            player.AttackInterval = 2f;
        }
    }
}
