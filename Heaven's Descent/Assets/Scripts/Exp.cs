using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Exp : MonoBehaviour
{
    private ScoreCount scoreCount;
    private float speed = 10f;
    private GameObject player;

    private bool coinCollected = false;
    private bool collisionHandled = false; // New flag to track collision handling
    private CollectExpSfx collectExpSfx;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        scoreCount = FindObjectOfType<ScoreCount>();
        collectExpSfx = FindObjectOfType<CollectExpSfx>();
    }

    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !collisionHandled) // Check if collision already handled
        {
            Destroy(gameObject);
            coinCollected = true;
            collectExpSfx.checkCoinCollect(coinCollected);
            scoreCount.AddKill();
            collisionHandled = true; // Set collisionHandled flag to true
        }
    }
}
