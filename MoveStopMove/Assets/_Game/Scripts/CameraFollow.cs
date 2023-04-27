using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] internal Transform target;
    public float smoothSpeed = 0.125f;
    [SerializeField] internal Vector3 offset;
    internal Transform TF;
    internal Player player;
    private void FixedUpdate()
    {
        Vector3 desiredPos = target.position + offset;
        Vector3 smoothPos = Vector3.Lerp(transform.position, desiredPos, smoothSpeed);
        transform.position = smoothPos;
        transform.LookAt(target);
    }
}
