using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gameplay : UICanvas
{
    private void Start()
    {
        LevelManager.Ins.SetPlayAliveText(LevelManager.Ins.playerAlive);
    }
    public void SettingButton()
    {
        UIManager.Ins.OpenUI<Setting>();
    }
}
