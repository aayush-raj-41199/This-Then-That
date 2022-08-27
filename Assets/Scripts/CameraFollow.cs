using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public GameObject player;
    public float smoothSpeed = 0.15f;
    public Vector3 offset = new Vector3(0, 0.53f, 0);

    private Transform playerPosition;

    void Start()
    {
        playerPosition = player.GetComponent<Transform>();
    }

    void LateUpdate()
    {
        Vector3 desiredPosition = new Vector3(0, playerPosition.position.y + offset.y, -10);
        transform.position = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);        
    }
}