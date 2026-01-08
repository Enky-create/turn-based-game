using UnityEngine;

public class GridObject
{
    private GridSystem gridSystem;
    private GridPosition gridPosition;
    public Transform StoredTransform {get;set;}
    public GridObject(GridPosition gridPosition, GridSystem gridSystem)
    {
        this.gridPosition = gridPosition;
        this.gridSystem = gridSystem;
    }
    public bool IsEmpty()
    {
        return StoredTransform == null;
    }
}
