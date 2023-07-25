using System.Collections.Generic;
using UnityEngine;

public class Pathfinding
{
    private HashSet<Vector2Int> validPositions;

    public List<Vector2Int> FindPath(Vector2Int start, Vector2Int target, HashSet<Vector2Int> validPositions)
    {
        this.validPositions = validPositions;

        List<Vector2Int> path = new List<Vector2Int>();

        if (!validPositions.Contains(target))
            return path;

        HashSet<Vector2Int> closedList = new HashSet<Vector2Int>();
        Dictionary<Vector2Int, Vector2Int> cameFrom = new Dictionary<Vector2Int, Vector2Int>();
        Dictionary<Vector2Int, float> costSoFar = new Dictionary<Vector2Int, float>();

        PriorityQueue<Vector2Int> openList = new PriorityQueue<Vector2Int>();
        openList.Enqueue(start, 0);

        costSoFar[start] = 0;

        while (openList.Count > 0)
        {
            Vector2Int current = openList.Dequeue();

            if (current == target)
                break;

            closedList.Add(current);

            List<Vector2Int> neighbors = GetNeighbors(current);

            foreach (Vector2Int neighbor in neighbors)
            {
                if (!validPositions.Contains(neighbor) || closedList.Contains(neighbor))
                    continue;

                float newCost = costSoFar[current] + GetStepCost(current, neighbor);

                if (!costSoFar.ContainsKey(neighbor) || newCost < costSoFar[neighbor])
                {
                    costSoFar[neighbor] = newCost;
                    float priority = newCost + GetHeuristicCost(neighbor, target);

                    if (!openList.Contains(neighbor))
                        openList.Enqueue(neighbor, priority);

                    cameFrom[neighbor] = current;
                }
            }
        }

        if (!cameFrom.ContainsKey(target))
            return path;

        Vector2Int currentPos = target;
        while (currentPos != start)
        {
            path.Insert(0, currentPos);
            currentPos = cameFrom[currentPos];
        }
        path.Insert(0, start);

        return path;
    }

    private float GetStepCost(Vector2Int from, Vector2Int to)
    {
        // Use 1 as the cost for each step
        return 1f;
    }

    private float GetHeuristicCost(Vector2Int from, Vector2Int to)
    {
        // Use the Manhattan distance as the heuristic
        return Mathf.Abs(to.x - from.x) + Mathf.Abs(to.y - from.y);
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
            Vector2Int neighborPos = position + direction;
            if (validPositions.Contains(neighborPos))
                neighbors.Add(neighborPos);
        }

        return neighbors;
    }

    private class PriorityQueue<T>
    {
        private List<(T item, float priority)> elements = new List<(T, float)>();

        public int Count => elements.Count;

        public void Enqueue(T item, float priority)
        {
            elements.Add((item, priority));
        }

        public T Dequeue()
        {
            int bestIndex = 0;
            for (int i = 0; i < elements.Count; i++)
            {
                if (elements[i].priority < elements[bestIndex].priority)
                {
                    bestIndex = i;
                }
            }

            T bestItem = elements[bestIndex].item;
            elements.RemoveAt(bestIndex);
            return bestItem;
        }

        public bool Contains(T item)
        {
            for (int i = 0; i < elements.Count; i++)
            {
                if (EqualityComparer<T>.Default.Equals(elements[i].item, item))
                {
                    return true;
                }
            }
            return false;
        }
    }
}
