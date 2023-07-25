using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawner : MonoBehaviour
{
    public GameObject playerPrefab; // Reference to the player prefab
    private RoomFirstDungeonGenerator dungeonGenerator; // Reference to the RoomFirstDungeonGenerator script

    public GameObject playerInstance;
    public GameObject SpawnPlayer()
    {
        dungeonGenerator = FindObjectOfType<RoomFirstDungeonGenerator>(); // Get the reference to the RoomFirstDungeonGenerator script
        dungeonGenerator.GenerateDungeon(); // Run PCG

        if (dungeonGenerator != null)
        {
            Vector2Int center = dungeonGenerator.randomRoomCenter;
            Vector2 spawnPosition = new Vector2(center.x, center.y);

            // Instantiate the player prefab
            GameObject player = Instantiate(playerPrefab, spawnPosition, Quaternion.identity);
            playerInstance = player;
            // Adjust the sorting order of the player sprite renderer
            Renderer playerRenderer = player.GetComponentInChildren<Renderer>();
            if (playerRenderer != null)
            {
                playerRenderer.sortingOrder = 50; // Set a high sorting order value to ensure it's on top of other objects
            }

            // Get the reference to the EnemySpawner script and set the necessary parameters
            EnemySpawner enemySpawner = FindObjectOfType<EnemySpawner>();
            enemySpawner.SetDungeonFloorPositions(dungeonGenerator.GetDungeonFloorPositions());
            enemySpawner.SetPlayerPosition(player.transform.position);

            return player;
        }
        else
        {
            Debug.LogError("Could not find RoomFirstDungeonGenerator script in the scene.");
            return null;
        }
    }

    // reference for AI pathfindings
    public Vector2 GetPlayerPosition()
    {
        if (playerInstance != null)
        {
            return playerInstance.transform.position;
        }
        else
        {
            return Vector2.zero;
        }
    }

}
