using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MissionWaypoint : GameUnit
{
    [SerializeField] internal Image imageInfo;
    [SerializeField] internal Image imageArrow;
    [SerializeField] internal Transform arrowImg;
    [SerializeField] internal TextMeshProUGUI expInfo;
    [SerializeField] internal TextMeshProUGUI targetName;
    [SerializeField] internal GameObject waypoint;
    [SerializeField] internal Character targetFollow;
    [SerializeField] internal Vector3 offset;
    [SerializeField] private RectTransform rectTransform;
    internal Camera cameraMain;

    public override void OnDespawn()
    {
        SimplePool.Despawn(this);
    }

    public override void OnInit()
    {
    }
    public void OnInit(Character character)
    {
        cameraMain = GameManager.Ins.mainCamera;
        targetFollow = character;
        targetName.SetText(character.name);
    }
    public void SetColor(Color color)
    {
        targetName.color = color;
        imageArrow.color = color;
        imageInfo.color = color;
    }
    public Vector3 ConvertWPtoCP()
    {
        Vector3 viewPos = cameraMain.WorldToViewportPoint(targetFollow.TF.position + offset);

        viewPos.x = Mathf.Clamp(viewPos.x, 0.05f, 0.95f);
        viewPos.y = Mathf.Clamp(viewPos.y, 0.05f, 0.95f);

        if (viewPos.z <= 0f)
        {
            viewPos = new Vector3(viewPos.x * -1f, viewPos.y * -1f, viewPos.z);
        }

        Vector3 screenPos = cameraMain.ViewportToScreenPoint(viewPos);

        if (screenPos.z <= 0f)
        {
            Vector3 canvasPos = screenPos + new Vector3(Screen.width / 2, Screen.height / 2, 0);

            return canvasPos;
        }
        else
        {
            Vector3 canvasPos = screenPos - new Vector3(Screen.width / 2, Screen.height / 2, 0);

            return canvasPos;
        }
    }
    private void Update()
    {
        expInfo.text = (targetFollow.exp).ToString();
        rectTransform.anchoredPosition = ConvertWPtoCP();
        RotationToTarget();

        Vector3 viewPos = cameraMain.WorldToViewportPoint(targetFollow.TF.position);

        if (
            (viewPos.x > 0.05f && viewPos.x < 0.95f) && (viewPos.y > 0.05f && viewPos.y < 0.95f)
        )
        {
            targetName.gameObject.SetActive(true);
            imageArrow.gameObject.SetActive(false);
        }
        else
        {
            imageArrow.gameObject.SetActive(true);
            targetName.gameObject.SetActive(false);
        }
    }
    private void RotationToTarget()
    {
        Vector3 directionToTarget = (targetFollow.TF.position - LevelManager.Ins.player.TF.position).normalized;
        float angleToTarget = TF.localPosition.x > 0 ? -Vector3.Angle(TF.forward, directionToTarget) : Vector3.Angle(TF.forward, directionToTarget);
        arrowImg.localRotation = Quaternion.Euler(0f, 0f, angleToTarget);
    }
}
