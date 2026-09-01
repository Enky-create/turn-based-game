using UnityEngine;
using UnityEngine.UIElements.Experimental;

public class PathNode
{
    private bool isWalkable = true;
    private GridPosition gridPosition;
    private int fCost;
    private int gCost;
    private int hCost;
    private PathNode cameFromNode;
    public PathNode(GridPosition gridPosition)
    {
        this.gridPosition = gridPosition;
    }
    public void CalculateFcost()
    {
        fCost = gCost + hCost;
    }
    public void SetGcost(int gCost)
    {
        this.gCost = gCost;
    }
    public void SetHcost(int hCost)
    {
        this.hCost = hCost;
    }
    public void SetCameFromNode(PathNode node)
    {
        cameFromNode = node;
    }
    public int GetGcost()
    {
        return gCost;
    }
    public int GetHcost()
    {
        return hCost;
    }
    public int GetFcost()
    {
        return fCost;
    }
    public GridPosition GetGridPosition()
    {
        return gridPosition;
    }
    public PathNode GetCameFromeNode()
    {
        return cameFromNode;
    }
    public void ResetCameFromNode() {
        cameFromNode = null;
    }
    public bool GetIsWalkable()
    {
        return isWalkable;
    }
    public void SetIsWalkable(bool value)
    {
        isWalkable = value;
    }
}
