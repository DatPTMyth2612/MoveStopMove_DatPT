using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkinShopUI : UICanvas
{
    [SerializeField] private GameObject mainMenu;
    [SerializeField] private GameObject headSkinTab;
    [SerializeField] private GameObject pantSkinTab;
    [SerializeField] private GameObject shieldSkinTab;
    [SerializeField] private GameObject skinTab;
    [SerializeField] private GameObject headContent;
    [SerializeField] private GameObject button;
    private void Start()
    {
        OnClickHeadSkinTab();
        Instantiate(button, headContent.transform);
    }
    public void OnClickHeadSkinTab()
    {
        headSkinTab.SetActive(true);
        pantSkinTab.SetActive(false);
        shieldSkinTab.SetActive(false);
        skinTab.SetActive(false);
    }
    public void OnClickPantSkinTab()
    {
        headSkinTab.SetActive(false);
        pantSkinTab.SetActive(true);
        shieldSkinTab.SetActive(false);
        skinTab.SetActive(false);
    }
    public void OnClickShieldSkinTab()
    {
        headSkinTab.SetActive(false);
        pantSkinTab.SetActive(false);
        shieldSkinTab.SetActive(true);
        skinTab.SetActive(false);
    }
    public void OnClickSkinTab()
    {
        headSkinTab.SetActive(false);
        pantSkinTab.SetActive(false);
        shieldSkinTab.SetActive(false);
        skinTab.SetActive(true);
    }
    public void CloseButton()
    {
        //UIManager.Ins.OpenUI<MainMenu>();
        mainMenu.SetActive(true);
        Close(0);
    }
}
