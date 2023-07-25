using System.Collections.Generic;
using UnityEngine;

public class AStarPathfinding : MonoBehaviour
{
    private class Node
    {
        public Vector2Int position;
        public Node parent;
        public int gCost;
        public int hCost;
        public int fCost => gCost + hCost;

        public Node(Vector2Int position, Node parent, int gCost, int hCost)
        {
            this.position = position;
            this.parent = parent;
            this.gCost = gCost;
            this.hCost = hCost;
        }
    }

    private RoomFirstDungeonGenerator dungeonGenerator;
    private HashSet<Vector2Int> walkablePositions;
    private HashSet<Vector2Int> visitedPositions;
    private List<Node> openList;

    private Vector2Int playerPosition;

    public List<Vector2Int> FindPath(Vector2Int start, Vector2Int target, HashSet<Vector2Int> walkableTiles)
    {
        dungeonGenerator = FindObjectOfType<RoomFirstDungeonGenerator>();
        walkablePositions = walkableTiles;

        visitedPositions = new HashSet<Vector2Int>();
        openList = new List<Node>();

        playerPosition = target;

        Node startNode = new Node(start, null, 0, CalculateHeuristic(start, target));
        openList.Add(startNode);

        while (openList.Count > 0)
        {
            Node currentNode = GetLowestFCostNode();
            openList.Remove(currentNode);

            if (currentNode.position == target)
            {
                return GeneratePath(currentNode);
            }

            visitedPositions.Add(currentNode.position);

            foreach (Vector2Int neighbor in GetNeighbors(currentNode.position))
            {
                if (visitedPositions.Contains(neighbor))
                    continue;

                int gCost = currentNode.gCost + CalculateDistanceCost(currentNode.position, neighbor);
                int hCost = CalculateHeuristic(neighbor, target);
                Node neighborNode = new Node(neighbor, currentNode, gCost, hCost);

                if (!AddOrUpdateOpenList(neighborNode))
                {
                    openList.Add(neighborNode);
                }
            }
        }

        // No valid path found
        return null;
    }

    private Node GetLowestFCostNode()
    {
        Node lowestFCostNode = openList[0];
        for (int i = 1; i < openList.Count; i++)
        {
            if (openList[i].fCost < lowestFCostNode.fCost ||
                (openList[i].fCost == lowestFCostNode.fCost && openList[i].hCost < lowestFCostNode.hCost))
            {
                lowestFCostNode = openList[i];
            }
        }
        return lowestFCostNode;
    }

    private int CalculateHeuristic(Vector2Int from, Vector2Int to)
    {
        return Mathf.Abs(from.x - to.x) + Mathf.Abs(from.y - to.y);
    }

    private int CalculateDistanceCost(Vector2Int from, Vector2Int to)
    {
        int distanceX = Mathf.Abs(from.x - to.x);
        int distanceY = Mathf.Abs(from.y - to.y);
        return distanceX + distanceY;
    }

    private bool AddOrUpdateOpenList(Node node)
    {
        for (int i = 0; i < openList.Count; i++)
        {
            if (openList[i].position == node.position)
            {
                if (node.gCost < openList[i].gCost)
                {
                    openList[i].gCost = node.gCost;
                    openList[i].parent = node.parent;
                }
                return true;
            }
        }
        return false;
    }

    private List<Vector2Int> GeneratePath(Node endNode)
    {
        List<Vector2Int> path = new List<Vector2Int>();
        Node currentNode = endNode;

        while (currentNode != null)
        {
            path.Add(currentNode.position);
            currentNode = currentNode.parent;
        }

        path.Reverse();
        return path;
    }

    private List<Vector2Int> GetNeighbors(Vector2Int position)
    {
        List<Vector2Int> neighbors = new List<Vector2Int>();
        Vector2Int[] directions =
        {
            Vector2Int.up,
            Vector2Int.down,
            Vector2Int.left,
            Vector2Int.right
        };

        foreach (Vector2Int direction in directions)
        {
            Vector2Int neighbor = position + direction;
            if (walkablePositions.Contains(neighbor))
            {
                neighbors.Add(neighbor);
            }
        }

        return neighbors;
    }
}
