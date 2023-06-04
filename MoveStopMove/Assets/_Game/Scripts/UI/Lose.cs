using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Lose : UICanvas
{
    public TextMeshProUGUI rank;
    public TextMeshProUGUI nameKiller;
    public void SetText(Character attacker)
    {
        rank.SetText("#" + (LevelManager.Ins.stage.playerAlive + 1).ToString());
        nameKiller.SetText(attacker.name.ToString());
        nameKiller.color = attacker.wayPoint.targetName.color;
    }
    public void HomeButton()
    {
        LevelManager.Ins.OnRetry();
        UIManager.Ins.OpenUI<MainMenu>();
        GameManager.Ins.ChangeState(GameState.MainMenu);
        GameManager.Ins.cameraFollow.SetUpCameraMainMenu();
        Close(0);
    }
}
