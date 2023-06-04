using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Cache 
{
    static Dictionary<Collider, CharacterCollider> m_Character = new Dictionary<Collider, CharacterCollider>();

    public static CharacterCollider GetCharacterInParent(Collider key)
    {
        if (!m_Character.ContainsKey(key))
        {
            //TODO: khong duoc dung GetComponentInParent
            CharacterCollider characterCollider = key.GetComponent<CharacterCollider>();

            if (characterCollider != null)
            {
                m_Character.Add(key, characterCollider);
            }
            else
            {
                return null;
            }
        }

        return m_Character[key];
    }
}
