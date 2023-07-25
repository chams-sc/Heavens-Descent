using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunRotate : MonoBehaviour
{
    [SerializeField]
    private float minMoveSpeed = 1f;
    [SerializeField]
    private float maxMoveSpeed = 5f;
    [SerializeField]
    private float followDelay = 0.5f; // Delay in seconds before the gun starts following the player
    [SerializeField]
    private float distanceThreshold = 5f; // Distance threshold where the gun moves at maxMoveSpeed
    
    private float rotateSpeed = 2f;
    private Rigidbody2D rb;
    private Camera cam;
    private Transform playerTransform; // Reference to the player's transform

    private Vector2 targetPosition; // The position the gun is aiming to reach

    void Start()
    {
        cam = Camera.main;
        rb = GetComponent<Rigidbody2D>();
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void FixedUpdate()
    {
        Vector2 mousePos = cam.ScreenToWorldPoint(Input.mousePosition);

        if (Time.timeSinceLevelLoad >= followDelay)
        {
            targetPosition = playerTransform.position;
            float distance = Vector2.Distance(rb.position, targetPosition);
            float currentMoveSpeed = Mathf.Lerp(minMoveSpeed, maxMoveSpeed, distance / distanceThreshold);
            currentMoveSpeed = Mathf.Clamp(currentMoveSpeed, minMoveSpeed, maxMoveSpeed);
            transform.position = Vector2.MoveTowards(transform.position, targetPosition, currentMoveSpeed * Time.deltaTime);
        }

        Vector2 lookDir = mousePos - rb.position;
        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg - 90f;
        rb.rotation = Mathf.Lerp(rb.rotation, angle, rotateSpeed);
    }
}