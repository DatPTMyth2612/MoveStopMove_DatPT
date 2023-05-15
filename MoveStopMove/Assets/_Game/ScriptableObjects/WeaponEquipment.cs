using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Weapon", menuName = "Scriptable Objects/Weapon")]
public class WeaponEquiqment : ScriptableObject
{
    public int weaponIndex;
    public GameObject weaponPrefab;
    public string weaponName;
    public string weaponDescription;
}
