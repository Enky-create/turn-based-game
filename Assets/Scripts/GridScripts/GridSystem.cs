using System;
using UnityEngine;
public class GridSystem<TGridObject>
{
    private int width;
    private int length;
    private float cellSize;
    private Vector3 originPosition;
    private float offset = 0.0f;
    private TGridObject [,] gridObjects;

    public bool IsInGrid(Vector3 position)
    {
        var gridPosition = GetGridPosition(position);
        return IsInGrid(gridPosition);
    }
    public bool IsInGrid(GridPosition gridPosition)
    {
        var result = gridPosition.x>=0 && gridPosition.x < width && gridPosition.z >= 0 && gridPosition.z<length;
        return result;
    }
    public GridSystem(int width, int length, float cellSize, Vector3 originPosition,
    Func<GridPosition,GridSystem<TGridObject>,TGridObject>createTGridObject)
    {
        this.width= width;
        this.length = length;
        this.cellSize = cellSize;
        offset = cellSize/2;
        this.originPosition = originPosition;
        gridObjects = new TGridObject[width,length];
        for(int x = 0; x < width; x++)
        {
            for(int z = 0; z < length; z++)
            {
                var gridPosition = new GridPosition(x,z);
                gridObjects[x,z] = createTGridObject(gridPosition, this);
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
    public float GetCellSize()
    {
        return cellSize;
    }
    public TGridObject GetGridObject(GridPosition gridPosition)
    {
        return gridObjects[gridPosition.x,gridPosition.z];
    }
    public bool TryGetGridObject(Vector3 worldPosition, out TGridObject gridObject)
    {
        var result = TryGetGridObject(GetGridPosition(worldPosition),out  TGridObject resultGridObject);
        gridObject = resultGridObject;
        return result;
    }

    public bool TryGetGridObject(GridPosition gridPosition, out TGridObject gridObject)
    {
        if (IsInGrid(gridPosition))
        {
            gridObject = GetGridObject(gridPosition);
            return true;
        }
        gridObject = default(TGridObject);
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
    
}
