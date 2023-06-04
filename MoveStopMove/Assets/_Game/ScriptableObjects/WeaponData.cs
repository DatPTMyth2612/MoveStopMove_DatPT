using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Weapon", menuName = "Scriptable Objects/Weapon")]
public class WeaponData : ScriptableObject
{
    public int weaponIndex;
    public Sprite weaponSprite;
    public GameObject weaponPrefab;
    public Weapon weapon;
    public string weaponName;
    public string weaponDescription;
    public float weaponSpeed;
    public float weaponExtraRange;
    public int priceWeapon;
}
