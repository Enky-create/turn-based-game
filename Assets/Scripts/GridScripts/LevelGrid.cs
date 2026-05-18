using System;
using UnityEngine;

public class LevelGrid : MonoBehaviour
{
    public static LevelGrid Instance { get; private set; }
    private GridSystem grid;
    [SerializeField] private VisualGridCell visualGridCell;

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("There is more than one UnitActionSystem "
                + transform + " - " + Instance);
            Destroy(gameObject);
            return;
        }
        Instance = this;
        grid = new GridSystem(10, 10, 2f, Vector3.zero);
    }
    void Start()
    {
        for (int x = 0; x < grid.GetWidth(); x++)
        {
            for (int z = 0; z < grid.GetLength(); z++)
            {
                var gridPosition = new GridPosition(x, z);
                Vector3 pos = grid.GetWorldPosition(gridPosition);
                VisualGridCell gridcell = Instantiate(visualGridCell, pos, Quaternion.identity);
                gridcell.SetGridObject(grid.GetGridObject(gridPosition));
            }
        }
    }
    public bool TryGetGridObject(GridPosition gridPosition, out GridObject gridObject)=> grid.TryGetGridObject(gridPosition,out gridObject);
    public bool TryGetGridObject(Vector3 worldPosition, out GridObject gridObject)=> grid.TryGetGridObject(worldPosition,out gridObject);
    public GridPosition GetGridPosition(Vector3 worldPosition)=>grid.GetGridPosition(worldPosition);
    public Vector3 GetWorldPosition(GridPosition gridPosition)=> grid.GetWorldPosition(gridPosition);
    public bool TryGetGridPosition(Vector3 worldPosition, out GridPosition gridPosition) => grid.TryGetGridPosition(worldPosition, out gridPosition);
    public void UnitMovedPosition(GridPosition oldPosition, GridPosition newPosition, Unit unit)
    {
        grid.TryAddUnit(newPosition,unit);
        grid.TryRemoveUnit(oldPosition,unit);
    }
    public bool TryAddUnit(GridPosition gridPosition, Unit unit)=>grid.TryAddUnit(gridPosition,unit);
    public int GetWidth() => grid.GetWidth();
    public int GetLength() => grid.GetLength();
    public float GetCellSize() => grid.GetCellSize();
}
