using NUnit.Framework;
using System;
using System.Collections.Generic;
using UnityEngine;

public class GridSystemVisual : MonoBehaviour
{
    [SerializeField] private GridVisualSingle gridVisualSingle;
    private int width;
    private int length;
    private LevelGrid levelgrid;
    private GridVisualSingle[,] gridVisuals;
    public enum VisualColors
    {
        White,
        Red,
        Yellow,
        Blue,
        LightRed
    }
    [Serializable]
    public struct VisualMaterial
    {
        public VisualColors color;
        public Material material;
    }
    [SerializeField] private List<VisualMaterial> visualMaterials;
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
        UnitActionSystem.Instance.OnSelectedActionChange+= OnSelectedActionChange;
        LevelGrid.Instance.UnitMovedPositionEventHandler+=LevelGrid_UnitMovedPositionEventHandler;
        UpdateGridVisuals();
    }
    private void OnSelectedActionChange(object sender, EventArgs args)
    {
        UpdateGridVisuals();
    }
    void OnDestroy()
    {
        UnitActionSystem.Instance.OnSelectedActionChange-= OnSelectedActionChange;
    }

    public void ShowVisualsOnCertainPositions(List<GridPosition> positions, Material material)
    {
        foreach(var position in positions)
        {
            gridVisuals[position.x, position.z].Show(material);
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
        HideAllGridPositions();
        var selectedAction = UnitActionSystem.Instance.GetSelectedAction();
        var validList = selectedAction.GetValidGridPositionList();
        Material material;
        switch (selectedAction)
        {
            default:
            case MoveAction:
                material=GetMaterialForColors(VisualColors.White).material;
            break;
            case TurnAction:
                material=GetMaterialForColors(VisualColors.Blue).material;
            break;
            case ShootAction shootAction:
                var unit = shootAction.GetUnit();
                var distance = shootAction.GetMaxShootDistance();
                var list = GetMaxDistanceGridPositionList(distance,unit);
                ShowVisualsOnCertainPositions(list,
                GetMaterialForColors(VisualColors.LightRed).material);
                material=GetMaterialForColors(VisualColors.Red).material;
            break;
        }
        
        ShowVisualsOnCertainPositions(validList,material);
    }
    private void LevelGrid_UnitMovedPositionEventHandler(object sender, EventArgs e)
    {
        UpdateGridVisuals();
    }
    private VisualMaterial GetMaterialForColors(VisualColors color)
    {
        var result = visualMaterials.Find(visual=>visual.color==color);
        
        return result;
    }
    public List<GridPosition> GetMaxDistanceGridPositionList(int maxDistance,Unit unit)
    {
        var validPositions = new List<GridPosition>();
        for (int x= -maxDistance; x<=maxDistance; x++)
        {
            for (int z= -maxDistance; z<=maxDistance; z++)
            {
                var offsetGridPosition = new GridPosition(x,z);
                var unitPositon = unit.GetCurrentGridPosition();
                var testGridPosition = offsetGridPosition + unitPositon;
                var worldTestPosition = LevelGrid.Instance.GetWorldPosition(testGridPosition);
                if(Vector3.Distance(unit.transform.position, worldTestPosition) > maxDistance 
                * LevelGrid.Instance.GetCellSize())
                {
                    continue;
                }
                if(LevelGrid.Instance.TryGetGridObject(testGridPosition, out GridObject gridObject))
                {
                    
                    validPositions.Add(testGridPosition);
                    
                }
            }
        }
        return validPositions;
    }
}
