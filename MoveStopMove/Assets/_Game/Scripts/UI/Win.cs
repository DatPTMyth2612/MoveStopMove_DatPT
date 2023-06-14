using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Win : UICanvas
{
    public void NextLevelButton()
    {

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
