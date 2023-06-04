using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using Unity.AI.Navigation;
using TMPro;
using UnityEngine.AI;

public class LevelManager : Singleton<LevelManager>
{
    [SerializeField] internal Bot botPrefab;
    [SerializeField] internal MissionWaypoint waypointPrefab;
    [SerializeField] internal NavMeshSurface navMeshSurface;
    [SerializeField] internal int playerAlive = 30;
    [SerializeField] internal int maxBot = 9;
    [SerializeField] private TextMeshProUGUI coinText;
    [SerializeField] internal Player playerPrefab;
    [SerializeField] internal ParticleSystem bloodExplosion;
    [SerializeField] internal ParticleSystem deathParticle;
    internal Player player;
    internal Stage stage;

    private Transform TF;
    public int coin;
    public Stage[] stagePrefabs;
    private void Awake()
    {
        coin = PlayerPrefs.GetInt("NumberOfCoins");
        coin = 1500;
    }
    public void SetCoinText()
    {
        coinText.SetText(coin.ToString());
    }
    public void Start()
    {
        LoadLevel(0);
    }
    public void LoadLevel(int stageLevel)
    {
        if (stage != null)
        {
            Destroy(stage.gameObject);
        }
        if (stageLevel < stagePrefabs.Length)
        {
            stage = Instantiate(stagePrefabs[stageLevel], Vector3.zero, Quaternion.identity);
            stage.characterInStage.Clear();
            stage.OnInit();
            NavMesh.RemoveAllNavMeshData();
            NavMesh.AddNavMeshData(stage.navMeshData);
            navMeshSurface = GetComponent<NavMeshSurface>(); 
            player = SimplePool.Spawn<Player>(playerPrefab, stage.startPoint.position, Quaternion.Euler(0, 180, 0));
            player.currentStage = stage;
            stage.characterInStage.Add(player);
            player.OnInit();
            MissionWaypoint waypoint = SimplePool.Spawn<MissionWaypoint>(LevelManager.Ins.waypointPrefab);
            waypoint.SetColor(player.bodyRenderer.material.color);
            waypoint.OnInit(player);
            player.wayPoint = waypoint;
        }
    }
    public void OnReset()
    {
        SimplePool.CollectAll();
    }
    internal void OnRetry()
    {
        OnReset();
        LoadLevel(0);
        UIManager.Ins.OpenUI<MainMenu>();
    }
    private void OnApplicationQuit()
    {
        PlayerPrefs.SetInt("NumberOfCoins", coin);
    }
}
