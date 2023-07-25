using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPlayer : MonoBehaviour
{
    [SerializeField] private Transform playerPosition;
    private RoomFirstDungeonGenerator dungeonGenerator; // Reference to the RoomFirstDungeonGenerator script

    // Start is called before the first frame update
    void Start()
    {
        dungeonGenerator = FindObjectOfType<RoomFirstDungeonGenerator>(); // Get the reference to the RoomFirstDungeonGenerator script
        dungeonGenerator.GenerateDungeon(); // Run PCG
        if (dungeonGenerator != null)
        {
            Vector2Int center = dungeonGenerator.randomRoomCenter;
            Vector2 spawnPosition = new Vector2(center.x, center.y);

            playerPosition.position = spawnPosition; // Assign the spawn position to the player's position

            // Get the reference to the EnemySpawner script and set the necessary parameters
            EnemySpawner enemySpawner = FindObjectOfType<EnemySpawner>();
            enemySpawner.SetDungeonFloorPositions(dungeonGenerator.GetDungeonFloorPositions());
            enemySpawner.SetPlayerPosition(playerPosition.position);
        }
        else
        {
            Debug.LogError("Could not find RoomFirstDungeonGenerator script in the scene.");
        }
    }

    public Vector2 GetPlayerPosition()
    {
        if (playerPosition != null)
        {
            return playerPosition.position;
        }
        else
        {
            return Vector2.zero;
        }
    }
}