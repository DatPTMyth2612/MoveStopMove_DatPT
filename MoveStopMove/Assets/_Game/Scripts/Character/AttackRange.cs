using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class AttackRange : MonoBehaviour
{
    [SerializeField] private Character character;
    [SerializeField] private SphereCollider radiusRatio;
    [SerializeField] private float radius = 1.28f;
    internal Transform TF;
    private void OnTriggerEnter(Collider other)
    {
        CharacterCollider otherCollider = Cache.GetCharacterInParent(other);
        if (other.CompareTag(ConstString.TAG_BOT))
        {
            character.IsFire = true;
            if (character.m_TargetsInRange == null||!character.m_TargetsInRange.Contains(other.transform))
            {
                character.m_TargetsInRange.Add(other.transform);
            }
            character.currentTarget = FindNearestEnemy();
            character.OnSelect();
            if (otherCollider.character.IsDead)
            {
                character.OnDeSelect();
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(ConstString.TAG_BOT))
        { 
            character.OnDeSelect();
        }
    }
    public Transform FindNearestEnemy()
    {
        Transform nearestEnemy = character.m_TargetsInRange[0];
        float minDistance = GetDistanceFromTarget(nearestEnemy.position);

        for (int i = 0; i < character.m_TargetsInRange.Count; i++)
        {
            Transform characterInRange = character.m_TargetsInRange[i];
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
    public float GetAttackRadius()
    {
        return radius * gameObject.transform.localScale.x;
    }
}
