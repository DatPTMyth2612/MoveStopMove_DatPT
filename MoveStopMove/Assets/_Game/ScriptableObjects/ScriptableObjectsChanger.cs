using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ScriptableObjectsChanger : MonoBehaviour
{
    [SerializeField] private WeaponData[] scriptableObjects;
    [SerializeField] private WeaponDisplay weaponDisplay;
    [SerializeField] private WeaponData currentWeapon;
    private int currentIndex;
    private void Awake()
    {
        Change(0);
    }

    public void Change(int _index)
    {
        currentIndex += _index;
        Debug.Log(currentIndex);
        
        if (currentIndex < 0) currentIndex = scriptableObjects.Length - 1;
        if (currentIndex > scriptableObjects.Length - 1) currentIndex = 0;
        for (int i = 0; i < scriptableObjects.Length; i++)
        {
            if (scriptableObjects[i].weaponIndex == currentIndex)
            {
                currentWeapon = scriptableObjects[i];
                break;
            }
        }
        if (weaponDisplay != null) weaponDisplay.UpdateWeapon(currentWeapon);
    }   
}
