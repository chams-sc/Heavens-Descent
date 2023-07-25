using System.Collections.Generic;
using UnityEngine;

public class EnemyAIWithoutDetection : MonoBehaviour
{
    private Pathfinding pathfinding;
    private List<Vector2Int> currentPath;
    private int currentPathIndex = 0;
    private bool isFollowingPath = false;

    private SpawnPlayer spawnPlayer;
    private RoomFirstDungeonGenerator dungeonGenerator;
    private PlayerHurtSfx playerHurtSfx;

    [SerializeField]
    private float followSpeed = 5f;


    private void Awake()
    {
        pathfinding = new Pathfinding();
        spawnPlayer = FindObjectOfType<SpawnPlayer>();
        dungeonGenerator = FindObjectOfType<RoomFirstDungeonGenerator>();
        playerHurtSfx = FindObjectOfType<PlayerHurtSfx>();
    }

    private void Update()
    {
        Vector2 playerPosition = spawnPlayer.GetPlayerPosition();

        if (playerPosition != Vector2.zero)
        {
            if (!isFollowingPath)
            {
                Vector2Int startPosition = new Vector2Int(Mathf.RoundToInt(transform.position.x), Mathf.RoundToInt(transform.position.y));
                Vector2Int targetPosition = new Vector2Int(Mathf.RoundToInt(playerPosition.x), Mathf.RoundToInt(playerPosition.y));
                currentPath = pathfinding.FindPath(startPosition, targetPosition, dungeonGenerator.GetDungeonFloorPositions());
                currentPathIndex = 0;
                isFollowingPath = true;
            }
            else
            {
                if (currentPathIndex < currentPath.Count)
                {
                    Vector2 targetPosition = currentPath[currentPathIndex];
                    transform.position = Vector2.MoveTowards(transform.position, targetPosition, followSpeed * Time.deltaTime);

                    if ((Vector2)transform.position == targetPosition)
                    {
                        currentPathIndex++;
                    }
                }
                else
                {
                    // Reached the end of the path
                    isFollowingPath = false;
                }
            }
        }
    }

    // Access PlayerHurtScriptSfx script to play the sound 
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerHurtSfx.checkIfPlayerHurt(true);          //--- transfer to takedamage method of health script; instanstiate PlayerHurtSfx first in start method
        }
    }

    //See enemy path to player
    private void OnDrawGizmos()
    {
        if (currentPath != null && currentPath.Count > 0)
        {
            Gizmos.color = Color.red;
            for (int i = currentPathIndex; i < currentPath.Count; i++)
            {
                Vector2 position = currentPath[i];
                Gizmos.DrawWireSphere(position, 0.2f);
                
                if (i > currentPathIndex)
                {
                    Vector2 previousPosition = currentPath[i - 1];
                    Gizmos.DrawLine(previousPosition, position);
                }
            }
        }
    }
}
