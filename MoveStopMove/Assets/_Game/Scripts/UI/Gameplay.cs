using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Gameplay : UICanvas
{
    [SerializeField] private TextMeshProUGUI playerAliveText;
    public void SettingButton()
    {
        GameManager.Ins.ChangeState(GameState.Pause);
        UIManager.Ins.OpenUI<Setting>();
    }
    private void Update()
    {
        playerAliveText.SetText("Alive " + LevelManager.Ins.stage.playerAlive.ToString());
    }
}
