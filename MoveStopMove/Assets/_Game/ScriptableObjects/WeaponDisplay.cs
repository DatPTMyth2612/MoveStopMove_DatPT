using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponDisplay : MonoBehaviour
{
    [SerializeField] private Text weaponName;
    [SerializeField] private Text weaponDescription;
    [SerializeField] private GameObject weaponModel;
    public void UpdateWeapon(WeaponData weaponEquiqment)
    {
        weaponName.text = weaponEquiqment.weaponName;
        weaponDescription.text = weaponEquiqment.weaponDescription;
        if(weaponModel.transform.childCount>0 )
        {
            Destroy(weaponModel.transform.GetChild(0).gameObject);
        }
        Instantiate(weaponEquiqment.weaponPrefab, weaponModel.transform.position, weaponModel.transform.rotation, weaponModel.transform);
    }
}
