using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridComponent : MonoBehaviour
{
    public LayerMask unwalkableMask;
    public float nodeRadius;
    public List<Node> path;
    public float radius;
    [SerializeField]
    public Node playerIn;
    [SerializeField]
    public Node enemyIn;

    Node[,] grid;
    int gridSizeX;
    int gridSizeY;

    private void Awake()
    {
        gridSizeX = 31;
        gridSizeY = 15;
    }

    private void Start()
    {
        CreateGrid();
    }

    private void CreateGrid()
    {
        int x = -1, y = 0;

        grid = new Node[gridSizeX, gridSizeY];

        for (float teta = 0; teta < Mathf.PI * 2; teta += Mathf.PI / 15)
        {
            x++;
            for (float py = 0; py < Mathf.PI; py += Mathf.PI / 15)
            {
                Vector3 worldPoint = new Vector3(
                    radius * Mathf.Cos(teta) * Mathf.Sin(py)
                    , radius * Mathf.Sin(teta) * Mathf.Sin(py)
                    , radius * Mathf.Cos(py));
                bool walkable = !(Physics.CheckSphere(worldPoint, nodeRadius, unwalkableMask));

                grid[x, y] = new Node(walkable, worldPoint, x, y);
                y++;
            }
            y = 0;
        }
    }

    public List<Node> GetNeighbours(Node node)
    {
        var neighbours = new List<Node>();

        for (int x = -1; x <= 1; x++)
        {
            for (int y = -1; y <= 1; y++)
            {
                if (x == 0 && y == 0)
                    continue;

                int checkX = node.gridX + x;
                int checkY = node.gridY + y;

                if (checkX >= 0 && checkX < gridSizeX && checkY >= 0 && checkY < gridSizeY)
                {
                    neighbours.Add(grid[checkX, checkY]);
                }
            }
        }

        return neighbours;
    }

    public Node FromWorldPoint(Vector3 worldPosition)
    {
        int y = Mathf.Abs((int)(Mathf.Atan2(worldPosition.y, worldPosition.z) / (Mathf.PI / 15)));
        int x = (int)(Mathf.Atan2(worldPosition.y, worldPosition.x) / (Mathf.PI / 15));
        if (x < 0) x += 30;        
        return grid[x, y];
    }

    private void OnDrawGizmos()
    {
        if (grid != null)
            foreach (Node n in grid)
            {
                if (n != null)
                {
                    Gizmos.color = n.walkable ? Color.blue : Color.red;
                    if (path!= null && path.Contains(n))
                    {
                        if (n == playerIn) Gizmos.color = Color.black;
                        else if (n == enemyIn) Gizmos.color = Color.black;
                        Gizmos.DrawSphere(n.worldPos, nodeRadius);
                    }
                    else Gizmos.DrawWireSphere(n.worldPos, nodeRadius);
                }
            }
    }
}
