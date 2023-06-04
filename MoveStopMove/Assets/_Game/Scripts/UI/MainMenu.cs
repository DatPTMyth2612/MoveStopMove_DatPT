using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : UICanvas
{
    //[SerializeField] private GameObject skinShop;
    private void Start()
    {
        LevelManager.Ins.SetCoinText();
        GameManager.Ins.cameraFollow.SetUpCameraMainMenu();
        //skinShop.SetActive(false);
    }
    public void PlayButton()
    {
        GameManager.Ins.ChangeState(GameState.Gameplay);
        //skinShop.SetActive(false);
        UIManager.Ins.OpenUI<Gameplay>();
        GameManager.Ins.inGame.gameObject.SetActive(true);
        Close(0);  
    }
    public void WeaponButton() 
    {
        UIManager.Ins.OpenUI<WeaponShopUI>();
        //skinShop.SetActive(false);
        Close(0);
    }
    public void SkinButton()
    {
        //UIManager.Ins.OpenUI<SkinShopUI>();
        //skinShop.SetActive(true);
        Close(0);
    }
}
