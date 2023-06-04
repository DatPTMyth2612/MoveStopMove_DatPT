using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    [SerializeField] Material materialDefault;
    [SerializeField] Material materialBlur;
    [SerializeField] MeshRenderer obstacle;
    // Start is called before the first frame update
    void Start()
    {
        obstacle = GetComponent<MeshRenderer>();
        materialDefault = obstacle.material;
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag(ConstString.TAG_PLAYER))
        {
            obstacle.material = materialBlur;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(ConstString.TAG_PLAYER))
        {
            obstacle.material = materialDefault;
        }
    }
}
