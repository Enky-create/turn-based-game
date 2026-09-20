using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GranadeAction : BaseAction
{
    [SerializeField] private int maxThrowDistance=7;
    [SerializeField] private Transform grenadeTransform;
    private Vector3 targetPosition;
    
    public override List<GridPosition> GetValidGridPositionList()
    {
        var validPositions = new List<GridPosition>();
        for (int x= -maxThrowDistance; x<=maxThrowDistance; x++)
        {
            for (int z= -maxThrowDistance; z<=maxThrowDistance; z++)
            {
                var offsetGridPosition = new GridPosition(x,z);
                var unitPositon = unit.GetCurrentGridPosition();
                var testGridPosition = offsetGridPosition + unitPositon;
                var worldTestPosition = LevelGrid.Instance.GetWorldPosition(testGridPosition);
                var unitWorldPosition = LevelGrid.Instance.GetWorldPosition(unitPositon);
                if(Vector3.Distance(unitWorldPosition, worldTestPosition) > maxThrowDistance 
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

    public override bool IsValidGridPosition(GridPosition gridPosition)
    {
        return GetValidGridPositionList().Contains(gridPosition);
    }

    protected override EnemyAIAction GetEnemyAIAction(GridPosition gridPosition)
    {
        return new EnemyAIAction{
            gridPosition = gridPosition,
            actionValue = 0
        };
    }
    public override void Execute(Action onActionDone, GridPosition gridPosition)
    {
        targetPosition = LevelGrid.Instance.GetWorldPosition(gridPosition);
        Grenade grenade = Instantiate(grenadeTransform,transform.position,Quaternion.identity).GetComponent<Grenade>();
        grenade.Setup(targetPosition,onActionDone);
        ActionStart(onActionDone);
    }
    public override void Execute(Action onActionDone)
    {
        var mousePosition = LevelGrid.Instance.GetGridPosition(MouseWorld.MousePosition());
        Execute(onActionDone, mousePosition);
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        actionName = "Grenade";
    }

    void Update()
    {
        if (!isActive)
        {
            return;
        }
    }
}
