using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.PlasticSCM.Editor.WebApi;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR;


public class WeaponShopUI : UICanvas
{
    public int currentWeaponIndex;
    public int equipWeaponIndex;
    public int maxWeaponAmount;
    private List<GameObject> weaponModels = new List<GameObject>();
    [SerializeField] private Player player;
    [SerializeField] private Text weaponName;
    [SerializeField] private Text weaponDescription;
    [SerializeField] private TextMeshProUGUI priceWeapon;
    [SerializeField] private Transform weaponContainer;
    [SerializeField] private GameObject selectBtn;
    [SerializeField] private GameObject equipBtn;
    [SerializeField] private GameObject pursBtn;
    //[SerializeField] private GameObject weaponUI;
    void Start()
    {
        player = LevelManager.Ins.player;
        equipBtn.SetActive(false);
        currentWeaponIndex = PlayerPrefs.GetInt("Selected Weapon", 0);
        equipWeaponIndex = PlayerPrefs.GetInt("EquipWeapon", 0);
        maxWeaponAmount = WeaponConfig.Ins.weapon.Length - 1;
        for (int i = 0; i <= maxWeaponAmount; i++)
        {
            GameObject weaponModel = Instantiate(WeaponConfig.Ins.weapon[i].weaponPrefab, weaponContainer);
            weaponModel.transform.localPosition = Vector3.down;
            weaponModel.transform.localRotation = Quaternion.Euler(0, 0, 160);
            weaponModels.Add(weaponModel);
            weaponModels[i].SetActive(false);
            PlayerPrefData.GetBool(WeaponConfig.Ins.weapon[i].weaponName);
            if (WeaponConfig.Ins.weapon[i].priceWeapon == 0)
            {
                PlayerPrefData.SetBool(WeaponConfig.Ins.weapon[i].weaponName, true);
            }
        }
        weaponModels[currentWeaponIndex].SetActive(true);
        UpdateWeaponUI();
    }
    public void ChangeWeaponPrevious()
    {
        weaponModels[currentWeaponIndex].SetActive(false);
        currentWeaponIndex--;
        if (currentWeaponIndex <= 0) currentWeaponIndex = 0;
        weaponModels[currentWeaponIndex].SetActive(true);
        if (PlayerPrefData.GetBool(WeaponConfig.Ins.weapon[currentWeaponIndex].weaponName))
        {
            PlayerPrefs.SetInt("Selected Weapon", currentWeaponIndex);
        }
        UpdateWeaponUI();
    }
    public void ChangeWeaponNext()
    {
        weaponModels[currentWeaponIndex].SetActive(false);
        currentWeaponIndex++;
        if (currentWeaponIndex >= maxWeaponAmount) currentWeaponIndex = maxWeaponAmount;
        weaponModels[currentWeaponIndex].SetActive(true);
        if (PlayerPrefData.GetBool(WeaponConfig.Ins.weapon[currentWeaponIndex].weaponName))
        {
            PlayerPrefs.SetInt("Selected Weapon", currentWeaponIndex);
        }
        UpdateWeaponUI();
    }
    public void BuyButton()
    {
        if(LevelManager.Ins.coin > WeaponConfig.Ins.weapon[currentWeaponIndex].priceWeapon)
        {
            LevelManager.Ins.coin -= WeaponConfig.Ins.weapon[currentWeaponIndex].priceWeapon;
            LevelManager.Ins.SetCoinText();
            PlayerPrefData.SetBool(WeaponConfig.Ins.weapon[currentWeaponIndex].weaponName, true);
        }
        UpdateWeaponUI();
    }
    public void EuipButton()
    {
        equipWeaponIndex = currentWeaponIndex;
        PlayerPrefs.SetInt("Selected Weapon", currentWeaponIndex);
        PlayerPrefs.SetInt("EquipWeapon", equipWeaponIndex);
        player.weaponBullet = WeaponConfig.Ins.weapon[currentWeaponIndex].weapon;
        player.attackRange.gameObject.transform.localScale = player.attackRangeDefault;
        player.EquipWeapon(equipWeaponIndex);
        player.CreateWeapon(currentWeaponIndex);
        UpdateWeaponUI();
    }
    public void CloseButton()
    {
        UIManager.Ins.OpenUI<MainMenu>();
        Close(0);
    } 
    private void UpdateWeaponUI()
    {
        if(PlayerPrefData.GetBool(WeaponConfig.Ins.weapon[currentWeaponIndex].weaponName))
        {
            pursBtn.SetActive(false);
            equipBtn.SetActive(true);
        }
        else
        {
            pursBtn.SetActive(true);
            equipBtn.SetActive(false);
            selectBtn.SetActive(false);
        }
        if(equipWeaponIndex == currentWeaponIndex) 
        {
            equipBtn.SetActive(false);
            selectBtn.SetActive(true);
        }
        weaponName.text = WeaponConfig.Ins.weapon[currentWeaponIndex].weaponName;
        weaponDescription.text = WeaponConfig.Ins.weapon[currentWeaponIndex].weaponDescription;
        priceWeapon.text = WeaponConfig.Ins.weapon[currentWeaponIndex].priceWeapon.ToString(); 
    }
}
