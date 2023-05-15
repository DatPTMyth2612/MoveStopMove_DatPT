using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using Unity.AI.Navigation;

public class LevelManager : Singleton<LevelManager>
{
    [SerializeField] internal Bot botPrefab;
    [SerializeField] internal NavMeshSurface navMeshSurface;
    [SerializeField] internal Stage stage;
    [SerializeField] internal int playerAlive = 30;
    [SerializeField] internal int maxBot = 10;

    public void Start()
    {
        navMeshSurface = GetComponent<NavMeshSurface>();
    }
}
