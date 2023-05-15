using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : UICanvas
{
    [SerializeField] private GameObject gamePlay;
    [SerializeField] private GameObject weaponShop;
    public void PlayButton()
    {
        Time.timeScale = 1f;
        gamePlay.SetActive(true);
        weaponShop.SetActive(false);
        Close(0);  
    }
    public void WeaponButton() 
    {
        //UIManager.Ins.OpenUI<WeaponShopUI>();
        gamePlay.SetActive(false);
        weaponShop.SetActive(true);
        Close(0);
    }
}
