using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] internal Transform target;
    [SerializeField] internal Vector3 offset;
    [SerializeField] private Transform mainMenu;
    public float smoothSpeed = 0.125f;
    internal Transform TF;

    private void Start()
    {
        target = LevelManager.Ins.player.transform;
    }
    private void FixedUpdate()
    {
        //Stopwatch
        if(GameManager.Ins.IsState(GameState.Gameplay))
        {
            Vector3 desiredPos = target.position + offset;
            Vector3 smoothPos = Vector3.Lerp(transform.position, desiredPos, smoothSpeed);
            transform.position = smoothPos;
            transform.LookAt(target);
        }
    }
    public void SetUpCameraMainMenu()
    {
        transform.position = mainMenu.position;
        transform.rotation = mainMenu.rotation;
    }
    public void SetUpCameraSkinShop()
    {

    }
}
