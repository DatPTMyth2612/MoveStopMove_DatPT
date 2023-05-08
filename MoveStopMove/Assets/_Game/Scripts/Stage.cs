using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Stage : MonoBehaviour
{
    [SerializeField] internal List<Character> characterInStage = new List<Character>();
    public Bot botPrefab;
    private NavMeshHit hit;
    internal int playerAlive = 30;
    internal int maxBot = 10;

    private void Start()
    {
        OnInit();
    }
    public void OnInit()
    {
        if (IsCanSpawnBot())
        {
            SpawnBot(maxBot);
        }
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
            if (Vector3.Distance(characterInStage[i].TF.position, hit.position) < (characterInStage[i].attackRange.GetAttackRadius() + 6f))
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
            bot.OnInit();
        }
        
    }
    public bool IsCanSpawnBot()
    {
        return playerAlive - 1 >= maxBot;
    }
}
