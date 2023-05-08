using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using Unity.AI.Navigation;

public class LevelManager : Singleton<LevelManager>
{
    [SerializeField] internal Bot botPrefab;
    [SerializeField] internal NavMeshSurface navMeshSurface;
    public void Start()
    {
        navMeshSurface = GetComponent<NavMeshSurface>();
    }
}
