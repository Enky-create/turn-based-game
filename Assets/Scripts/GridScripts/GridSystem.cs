using UnityEngine;
public class GridSystem
{
    private int width;
    private int length;
    private float cellSize;
    private Vector3 originPosition;
    private float offset = 0.0f;
    private GridObject [,] gridObjects;

    public bool IsInGrid(Vector3 position)
    {
        var gridPosition = GetGridPosition(position);
        var result = gridPosition.x>=0 && gridPosition.x < width && gridPosition.z >= 0 && gridPosition.z<length;
        return result;
    }
    public GridSystem(int width, int length, float cellSize, Vector3 originPosition)
    {
        this.width= width;
        this.length = length;
        this.cellSize = cellSize;
        offset = cellSize/2;
        this.originPosition = originPosition;
        gridObjects = new GridObject[width,length];
        for(int x = 0; x < width; x++)
        {
            for(int z = 0; z < length; z++)
            {
                var gridPosition = new GridPosition(x,z);
                gridObjects[x,z] = new GridObject(gridPosition, this);
            }
        }
    }
    public Vector3 GetWorldPosition(GridPosition gridPosition)
    {
        var noOriginPosition = new Vector3
        {
            x=gridPosition.x,
            y=0,
            z=gridPosition.z
        }*cellSize;
        return noOriginPosition + originPosition;
    }

    public Vector3 GetWorldPositionWithOffset(GridPosition gridPosition)
    {
        var noOriginPosition = new Vector3
        {
            x=gridPosition.x,
            y=0,
            z=gridPosition.z
        }*cellSize;
        var noOffset=noOriginPosition + originPosition;
        var worldPositionWithOffset = noOffset + new Vector3(offset,0,offset);
        return worldPositionWithOffset;
    }

    public GridPosition GetGridPosition(Vector3 worldPosition)
    {
        var noOriginPosition=worldPosition - originPosition;
        return new GridPosition
        (
            Mathf.RoundToInt(noOriginPosition.x/cellSize),
            Mathf.RoundToInt(noOriginPosition.z/cellSize)
        );
    }
    public int GetWidth()
    {
        return width;
    }
    public int GetLength()
    {
        return length;
    }
    public GridObject GetGridObject(GridPosition gridPosition)
    {
        return gridObjects[gridPosition.x,gridPosition.z];
    }
    public bool TryGetGetGridObject(Vector3 worldPosition, out GridObject gridObject)
    {
        if (IsInGrid(worldPosition))
        {
            gridObject = GetGridObject(GetGridPosition(worldPosition));
            return true;
        }
        gridObject = null;
        return false;
    }

    public bool TryGetGridPosition(Vector3 worldPosition, out GridPosition gridPosition)
    {
        if (IsInGrid(worldPosition))
        {
            gridPosition = GetGridPosition(worldPosition);
            return true;
        }
        gridPosition = new GridPosition(0, 0);
        return false;
    }
    public bool TrySetStoredObject(Vector3 worldPosition,Transform storedObject)
    {
        if (TryGetGetGridObject(worldPosition, out GridObject gridObject))
        {
            gridObject.StoredTransform = storedObject;
            return true;
        }
        return false;
    }
}
