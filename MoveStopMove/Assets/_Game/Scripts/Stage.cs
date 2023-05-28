using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Stage : GameUnit
{
    [SerializeField] internal List<Character> characterInStage = new List<Character>();
    public Bot botPrefab;
    private NavMeshHit hit;
    internal int playerAlive;
    internal int maxBot;

    private void Start()
    {
        OnInit();
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
            characterInStage.Add(bot);
            bot.OnInit();
            MissionWaypoint waypoint = SimplePool.Spawn<MissionWaypoint>(LevelManager.Ins.waypointPrefab);
            waypoint.targetFollow = bot;
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
    public override void OnInit()
    {
        playerAlive = LevelManager.Ins.playerAlive;
        maxBot = LevelManager.Ins.maxBot;
        characterInStage.Add(LevelManager.Ins.player);
        if (IsCanSpawnBot())
        {
            SpawnBot(maxBot);
        }
    }

    public override void OnDespawn()
    {

    }
}
