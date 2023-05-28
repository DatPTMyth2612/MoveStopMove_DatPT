using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "HairData", menuName = "ScriptableObjects/HairData", order = 2)]

public class HairData : ScriptableObject
{
    public Sprite hairImg;
    public string hairName;
    public GameObject hair;
    public string hairDescription;
    public int price;
    public bool isUnlocked;
}
