using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using Unity.AI.Navigation;
using TMPro;

public class LevelManager : Singleton<LevelManager>
{
    [SerializeField] internal Bot botPrefab;
    [SerializeField] internal MissionWaypoint waypointPrefab;
    [SerializeField] internal NavMeshSurface navMeshSurface;
    [SerializeField] internal Stage stagePrefab;
    [SerializeField] internal int playerAlive = 30;
    [SerializeField] internal int maxBot = 10;
    [SerializeField] private TextMeshProUGUI coinText;
    [SerializeField] private TextMeshProUGUI playerAliveText;
    [SerializeField] internal Player playerPrefab;
    internal Player player;
    internal Stage stage;

    private Transform TF;
    public int coin;
    private void Awake()
    {
        coin = PlayerPrefs.GetInt("NumberOfCoins");
        coin = 1500;
    }
    public void SetCoinText()
    {
        coinText.SetText(coin.ToString());
    }
    public void SetPlayAliveText(int numberOfplayer)
    {
        playerAliveText.SetText("Alive: " + numberOfplayer.ToString());
    }
    public void Start()
    {
        TF = gameObject.transform;
        stage = Instantiate(stagePrefab, Vector3.zero, Quaternion.identity,TF);
        //stage.characterInStage.Clear();
        stage.OnInit();
        navMeshSurface = GetComponent<NavMeshSurface>();
    }
    private void OnApplicationQuit()
    {
        PlayerPrefs.SetInt("NumberOfCoins", coin);
    }
}
