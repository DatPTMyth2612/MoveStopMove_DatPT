using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class AttackRange : MonoBehaviour
{
    [SerializeField] private Character character;
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Bot"))
        {
            character.IsAttack = true;
            if (character.TargetsInRange == null||!character.TargetsInRange.Contains(other.transform))
            {
                character.TargetsInRange.Add(other.transform);
            }
            character.currentTarget = FindNearestEnemy();
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Bot"))
        {
            character.IsAttack = false;
        }
    }
    public Transform FindNearestEnemy()
    {
        Transform nearestEnemy = character.TargetsInRange[0];
        float minDistance = GetDistanceFromTarget(nearestEnemy.position);

        for (int i = 0; i < character.TargetsInRange.Count; i++)
        {
            Transform characterInRange = character.TargetsInRange[i];
            if (Vector3.Distance(character.TF.position, characterInRange.position) < minDistance)
            {
                nearestEnemy = characterInRange;
            }
        }
        return nearestEnemy;
    }
    public float GetDistanceFromTarget(Vector3 targetPosition)
    {
        return Vector3.Distance(character.TF.position, targetPosition);
    }

}
