using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject enemyPrefab; // Reference to the enemy prefab
    [SerializeField]
    private GameObject enemyPrefab2; // Reference to the enemy prefab
    [SerializeField]
    private int minEnemiesToSpawn = 1; // Minimum number of enemies to spawn
    [SerializeField]
    private int maxEnemiesToSpawn = 3; // Maximum number of enemies to spawn
    [SerializeField]
    private float spawnRate = 0.5f; // Time interval between enemy spawns
    [SerializeField]
    private int remainingEnemies = 5; // Set number of enemies to spawn during the whole game

    private HashSet<Vector2Int> floorPositions; // Floor positions of the generated dungeon
    private Vector2 playerPosition; // Player's position
    private float spawnTimer; // Timer to control enemy spawns

    private void Start()
    {
        spawnTimer = spawnRate;
    }

    private void Update()
    {
        spawnTimer -= Time.deltaTime;
        if (spawnTimer <= 0f)
        {
            // Time to spawn enemies
            int numEnemies = Random.Range(minEnemiesToSpawn, maxEnemiesToSpawn + 1);

            for (int i = 0; i < numEnemies; i++)
            {
                Vector2 spawnPosition = GetRandomValidSpawnPosition();
                if (spawnPosition != Vector2.zero && remainingEnemies > 0)
                {
                    GameObject enemy = Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
                    GameObject enemy2 = Instantiate(enemyPrefab2, spawnPosition, Quaternion.identity);
                    remainingEnemies--;
                }               
            }

            spawnTimer = spawnRate;
        }
    }

    private Vector2Int GetRandomValidSpawnPosition()
    {
        int maxAttempts = 100;
        for (int i = 0; i < maxAttempts; i++)
        {
            Vector2Int randomPosition = GetRandomFloorPosition();
            if (IsValidSpawnPosition(randomPosition) && !HasWallAtPosition(randomPosition))
            {
                // Ensure the spawn position is not too close to the player and doesn't have a wall
                return randomPosition;
            }
        }

        Debug.LogWarning("Could not find a valid spawn position for enemy.");
        return Vector2Int.zero;
    }

    private bool HasWallAtPosition(Vector2 position)
    {
        // Perform a raycast to check if there is a wall at the given position
        RaycastHit2D hit = Physics2D.Raycast(position, Vector2.zero, 0f);
        return hit.collider != null && hit.collider.CompareTag("Wall");
    }

    private Vector2Int GetRandomFloorPosition()
    {
        int maxAttempts = 100;
        for (int i = 0; i < maxAttempts; i++)
        {
            Vector2Int randomPosition = floorPositions.GetRandomElement();
            if (IsValidSpawnPosition(randomPosition))
            {
                return randomPosition;
            }
        }

        Debug.LogWarning("Could not find a valid floor position for enemy spawn.");
        return Vector2Int.zero;
    }

    private bool IsValidSpawnPosition(Vector2Int position)
    {
        // Check if the position is within valid range and not obstructed by walls
        return floorPositions.Contains(position);
    }

    public void SetDungeonFloorPositions(HashSet<Vector2Int> floorPositions)
    {
        this.floorPositions = floorPositions;
    }

    public void SetPlayerPosition(Vector2 playerPosition)
    {
        this.playerPosition = playerPosition;
    }
}

public static class HashSetExtensions
{
    public static T GetRandomElement<T>(this HashSet<T> hashSet)
    {
        int index = Random.Range(0, hashSet.Count);
        var enumerator = hashSet.GetEnumerator();
        for (int i = 0; i <= index; i++)
        {
            enumerator.MoveNext();
        }
        return enumerator.Current;
    }
}