using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting.ReorderableList;
using UnityEngine;

public class GridObject
{
    private GridSystem gridSystem;
    private GridPosition gridPosition;
    private List<Unit> unitList;
    public GridObject(GridPosition gridPosition, GridSystem gridSystem)
    {
        unitList = new List<Unit>();
        this.gridPosition = gridPosition;
        this.gridSystem = gridSystem;
    }
    public bool IsEmpty()
    {
        return unitList.Count==0;
    }

    public override string ToString()
    {
        var resultString = $"{gridPosition} \n";
        foreach(Unit unit in unitList)
        {
            resultString+=unit.ToString() + "\n";
        }
        return resultString;
    }
    public void AddUnit(Unit unit)
    {
        unitList.Add(unit);
    }

    public void RemoveUnit(Unit unit)
    {
        unitList.Remove(unit);
    }
    public Unit GetFirstUnitInList()
    {
        if (unitList.Count > 0)
        {
            return unitList[0];
        }
        return null;
    }
}
