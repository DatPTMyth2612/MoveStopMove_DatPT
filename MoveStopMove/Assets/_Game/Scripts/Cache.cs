using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Cache 
{
    static Dictionary<Collider, Character> m_Character = new Dictionary<Collider, Character>();
    public static Character GetCharacter(Collider key)
    {
        if (!m_Character.ContainsKey(key))
        {
            Character character = key.GetComponentInParent<Character>();

            if (character != null)
            {
                m_Character.Add(key, character);
            }
            else
            {
                return null;
            }
        }

        return m_Character[key];
    }
}
