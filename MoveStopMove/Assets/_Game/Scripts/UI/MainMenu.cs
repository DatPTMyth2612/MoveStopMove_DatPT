using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : UICanvas
{
    [SerializeField] private GameObject gamePlay;
    [SerializeField] private GameObject weaponShop;
    [SerializeField] private GameObject skinShop;
    private void Start()
    {
        LevelManager.Ins.SetCoinText();
        gamePlay.SetActive(false);
        weaponShop.SetActive(false);
        skinShop.SetActive(false);
    }
    public void PlayButton()
    {
        Time.timeScale = 1f;
        gamePlay.SetActive(true);
        weaponShop.SetActive(false);
        skinShop.SetActive(false);
        Close(0);  
    }
    public void WeaponButton() 
    {
        //UIManager.Ins.OpenUI<WeaponShopUI>();
        gamePlay.SetActive(false);
        weaponShop.SetActive(true);
        skinShop.SetActive(false);
        Close(0);
    }
    public void SkinButton()
    {
        //UIManager.Ins.OpenUI<SkinShopUI>();
        gamePlay.SetActive(false);
        weaponShop.SetActive(false);
        skinShop.SetActive(true);
        Close(0);
    }
}
