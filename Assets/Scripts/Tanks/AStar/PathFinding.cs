using UnityEngine;
using System.Collections.Generic;

[RequireComponent(typeof(Rigidbody))]

public class PathFinding : MonoBehaviour
{
	Vector2 lastDirection;
	GridComponent grid;
	Rigidbody rb;

	public struct Data
	{
		public Node path;
		public int length;
	};

	Data data;

	void Awake()
	{
		grid = GetComponent<GridComponent>();
		rb = GetComponent<Rigidbody>();
	}

	void Start()
	{
		rb.useGravity = false;
		//rb.constraints = RigidbodyConstraints.FreezeRotation;
	}


	public Data FindPath(Vector3 startPos, Vector3 targetPos)
	{
		Node startNode = grid.FromWorldPoint(startPos);
		Node targetNode = grid.FromWorldPoint(targetPos);
		var openSet = new List<Node>();
		var closedSet = new HashSet<Node>();
		openSet.Add(startNode);

		while (openSet.Count > 0)
		{
			Node node = openSet[0];
			for (int i = 1; i < openSet.Count; i++)
			{
				if (openSet[i].fCost < node.fCost || openSet[i].fCost == node.fCost)
				{
					if (openSet[i].hCost < node.hCost)
						node = openSet[i];
				}
			}

			openSet.Remove(node);
			closedSet.Add(node);

			if (node == targetNode)
			{
				data.path = RetracePath(startNode, targetNode);
				return data;
			}

			foreach (Node neighbour in grid.GetNeighbours(node))
			{
				if (!neighbour.walkable || closedSet.Contains(neighbour))
				{
					continue;
				}

				int newCostToNeighbour = node.gCost + GetDistance(node, neighbour);

				if (newCostToNeighbour < neighbour.gCost || !openSet.Contains(neighbour))
				{
					neighbour.gCost = newCostToNeighbour;
					neighbour.hCost = GetDistance(neighbour, targetNode);
					neighbour.parent = node;

					if (!openSet.Contains(neighbour))
						openSet.Add(neighbour);
				}
			}
		}
		return data;
	}

	Node RetracePath(Node startNode, Node endNode)
	{
		var path = new List<Node>();
		Node currentNode = endNode;

		while (currentNode != startNode)
		{
			path.Add(currentNode);
			currentNode = currentNode.parent;
		}

		path.Reverse();
		data.length = path.Count;
		grid.path = path;

		return path.Count < 3 ? null : path[2];

	}

	int GetDistance(Node nodeA, Node nodeB)
	{
		int dstX = Mathf.Abs(nodeA.gridX - nodeB.gridX);
		int dstY = Mathf.Abs(nodeA.gridY - nodeB.gridY);

		if (dstX > dstY)
			return 14 * dstY + 10 * (dstX - dstY);
		return 14 * dstX + 10 * (dstY - dstX);
	}
}