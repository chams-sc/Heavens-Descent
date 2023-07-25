using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private GameObject playerInstance; // Reference to the spawned player object
    private Transform target; // Reference to the player's transform
    private float smoothTime = 0.25f;
    private Vector3 velocity = Vector3.zero;
    [SerializeField] private Vector3 offset =  new Vector3(0f, 0f, -10f); // Offset between the camera and the player


    private void Start()
    {
        if (playerInstance != null)
        {
            target = playerInstance.transform; // Set the player's transform as the target
            transform.position = new Vector3(target.position.x, target.position.y, transform.position.z) + new Vector3(offset.x, offset.y, 0);
        }
        else
        {
            Debug.LogError("Player failed to spawn.");
        }
    }


    private void LateUpdate()
    {
        if (target != null)
        {
            Vector3 targetPosition = target.position + offset;
            transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);
        }
    }
}
