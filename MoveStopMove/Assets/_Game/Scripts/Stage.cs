using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Stage : MonoBehaviour
{
    [SerializeField] internal List<Character> characterInStage = new List<Character>();
    [SerializeField] internal Color[] botColors;
    public NavMeshData navMeshData;
    public Bot botPrefab;
    public Transform startPoint;
    private NavMeshHit hit;
    internal int playerAlive;
    internal int maxBot;
    internal List<Color> characterColorAvaible = new List<Color>();
    public string[] playerNames =
    {
        "Abby",
        "Abbye",
        "Angela",
        "Caryln",
        "Cicely",
        "Cicily",
        "Ciel",
        "Cilka",
        "Cinda",
        "Cindee",
        "Dalila",
        "Dallas",
        "Daloris",
        "Damara",
        "Editha",
        "Edithe",
        "Ediva",
        "Edna",
        "Edwina",
        "Edy",
        "Edyth",
        "Edythe",
        "Effie",
        "Eileen",
        "Jodie",
        "Jody",
        "Joeann",
        "Joela",
        "Joelie",
        "Joell",
        "Joella",
        "Joelle",
        "Joellen",
        "Joelly",
        "Joellyn",
    };
    public void OnInit()
    {
        playerAlive = LevelManager.Ins.playerAlive;
        maxBot = LevelManager.Ins.maxBot;
        characterColorAvaible.AddRange(botColors);
        if (IsCanSpawnBot())
        {
            SpawnBot(maxBot);
        }
        SpawnGift(4);
    }

    public Vector3 RandomPointInStage()
    {
        // Get bounds stage
        Bounds stageBounds = navMeshData.sourceBounds;
        // Random x
        float rx = Random.Range(stageBounds.min.x, stageBounds.max.x);
        // Random z
        float rz = Random.Range(stageBounds.min.z, stageBounds.max.z);
        // Return random poin in stage
        return new Vector3(rx, 0.9f, rz);
    }
    public bool IsHasTargetInRange()
    {
        int numberCharacterInStage = characterInStage.Count;
        bool isInTarget = false;

        for (int i = 0; i < numberCharacterInStage; i++)
        {
            if ((Vector3.Distance(characterInStage[i].TF.position, hit.position) < (6f + characterInStage[i].attackRange.GetAttackRadius())))
            {
                isInTarget = true;
                break;
            }
        }
        return isInTarget;
    }
    public Vector3 GetPointToSpawn()
    {
        bool isContinueSearch = true;

        while (isContinueSearch)
        {
            if (NavMesh.SamplePosition(RandomPointInStage(), out hit, 1.0f, NavMesh.AllAreas))
            {
                if (!IsHasTargetInRange())
                {
                    isContinueSearch = false;
                    break;
                }
            }
        }
        return hit.position;
    }
    public void SpawnBot(int numberBot)
    {
        for (int i = 1; i <= numberBot; i++)
        {
            Vector3 pointToSpawn = GetPointToSpawn();
            Bot bot = SimplePool.Spawn<Bot>(LevelManager.Ins.botPrefab, pointToSpawn, Quaternion.identity);
            bot.name = $"{playerNames[Random.Range(0, playerNames.Length)]}";
            Color newColor = characterColorAvaible[0];
            characterColorAvaible.Remove(newColor);
            bot.ChangeColorBody(newColor);
            characterInStage.Add(bot);
            bot.currentStage = this;
            bot.OnInit();
            MissionWaypoint waypoint = SimplePool.Spawn<MissionWaypoint>(LevelManager.Ins.waypointPrefab);
            waypoint.OnInit(bot);
            waypoint.SetColor(newColor);
            bot.wayPoint = waypoint;
        }
    }
    public void SpawnGift(int numberGift)
    {
        for(int i =1; i<= numberGift;i++)
        {
            Vector3 pointToSpawn = GetPointToSpawn();
            Gift gift = SimplePool.Spawn<Gift>(PoolType.Gift,pointToSpawn + new Vector3(0,5,0), Quaternion.identity);
            gift.OnInit();
        }
    }
    public bool IsCanSpawnBot()
    {
        return playerAlive - 1 >= maxBot;
    }
    public void OnCharacterDie(Character characterDie)
    {
        characterInStage.Remove(characterDie);
        playerAlive--;
        if (playerAlive == 1)
        {
            CheckWinStage();
        }
        if (characterDie is Bot)
        {   
            if (IsCanSpawnBot())
            {
                SpawnBot(1);
            }
        }
    }

    public void CheckWinStage()
    {
        if(characterInStage.Count == 1)
        {
            if (characterInStage[0] is Player)
            {
                Debug.Log("win");
                //TO DO ins ui win
                UIManager.Ins.OpenUI<Win>();
                GameManager.Ins.IsState(GameState.Pause);
            }
        }
    }
    
}
