using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class WeaponShopUI : UICanvas
{
    public int currentWeaponIndex;
    public int maxWeaponAmount;
    private List<GameObject> weaponModels = new List<GameObject>();
    [SerializeField] private Text weaponName;
    [SerializeField] private Text weaponDescription;
    [SerializeField] private Transform weaponContainer;
    [SerializeField] private GameObject gamePlay;
    //[SerializeField] private GameObject weaponUI;
    void Start()
    {
        currentWeaponIndex = PlayerPrefs.GetInt("Selected Weapon", 0);
        maxWeaponAmount = WeaponConfig.Ins.weapon.Length - 1;
        for (int i = 0; i <= maxWeaponAmount; i++)
        {
            GameObject weaponModel = Instantiate(WeaponConfig.Ins.weapon[i].weaponPrefab, weaponContainer);
            weaponModels.Add(weaponModel);
        }
        UpdateWeaponUI();
    }
    public void ChangeWeaponPrevious()
    {
        currentWeaponIndex--;
        if (currentWeaponIndex <= 0) currentWeaponIndex = 0;
        PlayerPrefs.SetInt("Selected Weapon", currentWeaponIndex);
        UpdateWeaponUI();
    }
    public void ChangeWeaponNext()
    {
        currentWeaponIndex++;
        if (currentWeaponIndex >= maxWeaponAmount) currentWeaponIndex = maxWeaponAmount;
        PlayerPrefs.SetInt("Selected Weapon", currentWeaponIndex);
        UpdateWeaponUI();
    }
    public void BuyButton()
    {
        PlayerPrefs.SetInt("Selected Weapon", currentWeaponIndex);
    }
    public void CloseButton()
    {
        Time.timeScale = 1f;
        gamePlay.SetActive(true);
        Close(0);
    }
    private void UpdateWeaponUI()
    {
        weaponName.text = WeaponConfig.Ins.weapon[currentWeaponIndex].weaponName;
        weaponDescription.text = WeaponConfig.Ins.weapon[currentWeaponIndex].weaponDescription;
        for (int i = 0; i <= maxWeaponAmount; i++)
        {
            if (currentWeaponIndex == i)
            {
                weaponModels[i].SetActive(true);
            }
            else
            {
                weaponModels[i].SetActive(false);
            }
        }
    }
}
