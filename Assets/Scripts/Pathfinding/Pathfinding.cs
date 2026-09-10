using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class Pathfinding : MonoBehaviour
{
    private const int DIAGONAL_MOVE_COST = 14;
    private const int MOVE_STRAIGHT_COST = 10;

    [SerializeField] private PathfindingVisual pathfindingVisual;
    [SerializeField] private LayerMask obstacleLayerMask;
    private GridSystem<PathNode> grid;
    public static Pathfinding Instance;
    void Awake()
    {
        if (Instance!=null)
        {
            Debug.LogError("There's more then one instance of Pathfinding" + transform);
            Destroy(gameObject);
            return;
        }
        Instance = this;
        
    }

    public void Setup(int width, int length, float cellSize, Vector3 originPosition)
    {
        grid = new GridSystem<PathNode>(width, length, cellSize, originPosition,
         (GridPosition p,GridSystem<PathNode> g )=>new PathNode(p));
        for (int x = 0; x < grid.GetWidth(); x++)
        {
            for (int z = 0; z < grid.GetLength(); z++)
            {
                var gridPosition = new GridPosition(x, z);
                Vector3 pos = grid.GetWorldPosition(gridPosition);
                var raycastOffset = 5;
                if(Physics.SphereCast(pos + Vector3.down*raycastOffset, 1f, Vector3.up,out RaycastHit hitInfo, 2*raycastOffset, obstacleLayerMask))
                {
                    PathNode pathNode = grid.GetGridObject(gridPosition);
                    pathNode.SetIsWalkable(false);
                }
            }
        }
    }
    void Start()
    {
        //test objects
        // for (int x = 0; x < grid.GetWidth(); x++)
        // {
        //     for (int z = 0; z < grid.GetLength(); z++)
        //     {
        //         var gridPosition = new GridPosition(x, z);
        //         Vector3 pos = grid.GetWorldPosition(gridPosition);
        //         PathfindingVisual gridcell = Instantiate(pathfindingVisual, pos, Quaternion.identity);
        //         gridcell.SetPathNode(grid.GetGridObject(gridPosition));
        //     }
        // }
    }
    public List<GridPosition> FindPath(GridPosition pointA, GridPosition pointB, out int length)
    {
        List<PathNode> openList = new List<PathNode>();
        List<PathNode> closedList = new List<PathNode>();
        PathNode startNode = grid.GetGridObject(pointA);
        //reset all nodes
        for (int x = 0; x < grid.GetWidth(); x++)
        {
            for (int z = 0; z < grid.GetLength(); z++)
            {
                var gridPosition = new GridPosition(x, z);
                PathNode pathnode = grid.GetGridObject(gridPosition);
                pathnode.SetGcost(int.MaxValue);
                pathnode.SetHcost(0);
                pathnode.CalculateFcost();
                pathnode.ResetCameFromNode();
            }
        }
        startNode.SetHcost(CalculateDistance(pointA,pointB));
        startNode.SetGcost(0);
        startNode.CalculateFcost();
        openList.Add(startNode);
        while(openList.Count>0){
            var lowestFcostNode = GetLowestFcostNode(openList);
            if (lowestFcostNode.GetGridPosition() == pointB)
            {
                length = lowestFcostNode.GetFcost();
                return CalculatePath(lowestFcostNode);
            }
            openList.Remove(lowestFcostNode);
            closedList.Add(lowestFcostNode);
            foreach(PathNode pathNode in GetNeighbourList(lowestFcostNode))
            {
                if (closedList.Contains(pathNode))
                {
                    continue;
                }
                if (!pathNode.GetIsWalkable())
                {
                    closedList.Add(pathNode);
                    continue;
                }
                var tentativeGcost = lowestFcostNode.GetGcost() + CalculateDistance(lowestFcostNode.GetGridPosition(),
                pathNode.GetGridPosition());
                if(tentativeGcost < pathNode.GetGcost())
                {
                    pathNode.SetCameFromNode(lowestFcostNode);
                    pathNode.SetGcost(tentativeGcost);
                    pathNode.SetHcost(
                        CalculateDistance(pathNode.GetGridPosition(),
                        pointB));
                    pathNode.CalculateFcost();
                    openList.Add(pathNode);
                }
            }
        }
        length = 0;
        return null;
    }
    public bool HasPath(GridPosition startPosition, GridPosition endPosition)
    {
        return FindPath(startPosition,endPosition, out int length ) is not null;
    }
    private int CalculateDistance(GridPosition pointA,GridPosition pointB)
    {
        var distance = pointA - pointB;
        var xdistance = Mathf.Abs(distance.x);
        var zdistance = Mathf.Abs(distance.z);
        var diagonalDistance = Mathf.Min(xdistance,zdistance);
        var remaining = Mathf.Abs(xdistance-zdistance);
        return diagonalDistance*DIAGONAL_MOVE_COST + remaining*MOVE_STRAIGHT_COST;
    }
    private List<PathNode> GetNeighbourList(PathNode pathNode)
    {
        var resultingList = new List<PathNode>();
        var centerNodePosittion = pathNode.GetGridPosition();
        if (grid.TryGetGridObject(new GridPosition(centerNodePosittion.x-1,centerNodePosittion.z),
        out PathNode node))
        {
            resultingList.Add(node);
        }
        if (grid.TryGetGridObject(new GridPosition(centerNodePosittion.x-1,centerNodePosittion.z+1),
        out PathNode node1))
        {
            resultingList.Add(node1);
        }
        if (grid.TryGetGridObject(new GridPosition(centerNodePosittion.x-1,centerNodePosittion.z-1),
        out PathNode node2))
        {
            resultingList.Add(node2);
        }
        if (grid.TryGetGridObject(new GridPosition(centerNodePosittion.x,centerNodePosittion.z-1),
        out PathNode node3))
        {
            resultingList.Add(node3);
        }
        if (grid.TryGetGridObject(new GridPosition(centerNodePosittion.x,centerNodePosittion.z+1),
        out PathNode node4))
        {
            resultingList.Add(node4);
        }
        if (grid.TryGetGridObject(new GridPosition(centerNodePosittion.x+1,centerNodePosittion.z),
        out PathNode node5))
        {
            resultingList.Add(node5);
        }
        if (grid.TryGetGridObject(new GridPosition(centerNodePosittion.x+1,centerNodePosittion.z+1),
        out PathNode node6))
        {
            resultingList.Add(node6);
        }
        if (grid.TryGetGridObject(new GridPosition(centerNodePosittion.x+1,centerNodePosittion.z-1),
        out PathNode node7))
        {
            resultingList.Add(node7);
        }
        return resultingList;
    }
    private PathNode GetLowestFcostNode(List<PathNode> openList)
    {
        var lowestFcostNode = openList[0];
        foreach (PathNode node in openList)
        {
            if (lowestFcostNode.GetFcost() > node.GetFcost())
            {
                lowestFcostNode = node;
            }
        }
        return lowestFcostNode;
    }
    private List<GridPosition> CalculatePath(PathNode endNode)
    {
        var finalPath = new List<GridPosition>();
        finalPath.Add(endNode.GetGridPosition());
        var currentNode = endNode;
        while (currentNode.GetCameFromeNode() is not null)
        {
            currentNode = currentNode.GetCameFromeNode();
            finalPath.Add(currentNode.GetGridPosition());
        }
        finalPath.Reverse();
        return finalPath;
    }
}
