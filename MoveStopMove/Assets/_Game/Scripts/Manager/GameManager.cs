using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using UnityEngine.Events;

public enum GameState { MainMenu, Gameplay, Pause }

public class GameManager : Singleton<GameManager>
{
    [SerializeField] internal FloatingJoystick joystick;
    [SerializeField] internal CameraFollow cameraFollow;
    [SerializeField] internal Camera mainCamera;
    [SerializeField] internal Canvas inGame;
    private GameState gameState;

    private void Start()
    {
        ChangeState(GameState.MainMenu);
        UIManager.Ins.OpenUI<MainMenu>();  
        inGame.gameObject.SetActive(false);
    }

    public void ChangeState(GameState gameState)
    {
        this.gameState = gameState;
    }

    public bool IsState(GameState gameState)
    {
        return this.gameState == gameState;
    }
}
