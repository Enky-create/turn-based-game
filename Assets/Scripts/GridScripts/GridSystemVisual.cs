using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class GridSystemVisual : MonoBehaviour
{
    [SerializeField] private GridVisualSingle gridVisualSingle;
    private int width;
    private int length;
    private LevelGrid levelgrid;
    private GridVisualSingle[,] gridVisuals;
    void Awake()
    {
        levelgrid = LevelGrid.Instance;
        width = levelgrid.GetWidth();
        length = levelgrid.GetLength();
        gridVisuals = new GridVisualSingle[width, length];
    }
    private void Start()
    {
        for(int x=0; x < width; x++)
        {
            for(int z=0; z < length; z++)
            {
                var gridPosition = new GridPosition(x, z);
                var worldPosition = levelgrid.GetWorldPosition(gridPosition);
                var newGridVisualSingle = Instantiate(gridVisualSingle, worldPosition, Quaternion.identity)
                    .transform.GetComponent<GridVisualSingle>();
                gridVisuals[x, z] = newGridVisualSingle;
            }
        }
    }
    private void Update()
    {
        UpdateGridVisuals();
    }

    public void ShowVisualsOnCertainPositions(List<GridPosition> positions)
    {
        foreach(var position in positions)
        {
            gridVisuals[position.x, position.z].Show();
        }
    }
    public void HideAllGridPositions()
    {
        foreach (var visual in gridVisuals)
        {
            visual.Hide();
        }
    }
    public void UpdateGridVisuals()
    {
        var validList = UnitActionSystem.Instance.GetSelectedUnit().GetMoveAction().GetValidGridPositionList();
        HideAllGridPositions();
        ShowVisualsOnCertainPositions(validList);
    }
}
