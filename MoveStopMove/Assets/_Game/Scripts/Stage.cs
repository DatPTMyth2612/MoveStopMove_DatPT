using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Stage : GameUnit
{
    [SerializeField] internal List<Character> characterInStage = new List<Character>();
    [SerializeField] internal Color[] botColors;
    public Bot botPrefab;
    private NavMeshHit hit;
    internal int playerAlive;
    internal int maxBot;
    internal List<Color> characterColorAvaible = new List<Color>();
    public string[] playerNames =
    {
        "Abby",
        "Abbye",
        "Abigael",
        "Abigail",
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
    public override void OnInit()
    {
        playerAlive = LevelManager.Ins.playerAlive;
        maxBot = LevelManager.Ins.maxBot;
        Player playerObj = SimplePool.Spawn<Player>(LevelManager.Ins.playerPrefab, new Vector3(0, 1.5f, 0), Quaternion.identity);
        LevelManager.Ins.player = playerObj;
        characterInStage.Add(playerObj);
        playerObj.OnInit();
        MissionWaypoint waypoint = SimplePool.Spawn<MissionWaypoint>(LevelManager.Ins.waypointPrefab);
        waypoint.targetFollow = playerObj;
        waypoint.OnInit();
        characterColorAvaible.AddRange(botColors);
        if (IsCanSpawnBot())
        {
            SpawnBot(maxBot);
        }
    }

    public override void OnDespawn()
    {

    }
    public Vector3 RandomPointInStage()
    {
        // Get bounds stage
        Bounds stageBounds = LevelManager.Ins.navMeshSurface.navMeshData.sourceBounds;
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
            if (!(Vector3.Distance(characterInStage[i].TF.position, hit.position) > 6f))
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
            waypoint.targetFollow = bot;
            waypoint.targetName.SetText(bot.name.ToString());
            waypoint.targetName.color = newColor;
            waypoint.imageInfo.color = newColor;
            waypoint.imageArrow.color = newColor;
            waypoint.OnInit();
            bot.wayPoint = waypoint;
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
        LevelManager.Ins.SetPlayAliveText(playerAlive);
        if (playerAlive == 1)
        {
            CheckWinStage();
        }
        if (characterDie is Bot)
        {   
            if (IsCanSpawnBot())
            {
                Debug.Log("Spawn");
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
            }
        }
    }
    
}
