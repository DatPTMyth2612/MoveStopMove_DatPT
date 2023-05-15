using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PantsData", menuName = "ScriptableObjects/PantsData", order = 1)]
public class PantsData : ScriptableObject
{
    public string pantName;
    public Material pantMaterial;
    public string pantDescription;
}
