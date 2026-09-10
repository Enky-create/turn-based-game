using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MoveAction : BaseAction
{
    private List<GridPosition> movePositionList;
    [SerializeField] private int maxMoveDistance;
    [SerializeField] private int speed = 7;
    [SerializeField] private int rotationSpeed = 10;
    private int currentGridPositionIndex;
    protected override void Awake()
    {
        base.Awake();
        actionName = "Move";
    }
    private void Start()
    {
        //targetPosition = unit.GetCurrentGridPosition();
    }
    void Update()
    {
        if (!isActive)
        {
            return;
        }
        var stoppingDistance = 0.1;
        var worldPosition = LevelGrid.Instance.GetWorldPosition(movePositionList[currentGridPositionIndex]);
        if (Vector3.Distance(transform.position, worldPosition) > stoppingDistance)
        {
            Vector3 moveDirection = (worldPosition - this.transform.position).normalized;
            this.transform.position += moveDirection * speed * Time.deltaTime;
            OnActionStart?.Invoke(this, EventArgs.Empty);
            transform.forward = Vector3.Lerp(transform.forward, moveDirection, Time.deltaTime * rotationSpeed);

        }
        else
        {
            currentGridPositionIndex++;
            if (currentGridPositionIndex >= movePositionList.Count)
            {
                OnActionEnd?.Invoke(this,EventArgs.Empty);
                ActionEnd();
            }
        }
    }
    private void Move(Vector3 worldPosition)
    {
        var newTargetPosition = LevelGrid.Instance.GetGridPosition(worldPosition);
        this.movePositionList = Pathfinding.Instance.FindPath(unit.GetCurrentGridPosition(),newTargetPosition,out int length);
        currentGridPositionIndex = 0;
    }
    
    public override bool IsValidGridPosition(GridPosition gridPosition)
    {
        return GetValidGridPositionList().Contains(gridPosition);
    }
    
    public override List<GridPosition> GetValidGridPositionList()
    {
        var validPositions = new List<GridPosition>();
        for (int x= -maxMoveDistance; x<=maxMoveDistance; x++)
        {
            for (int z= -maxMoveDistance; z<=maxMoveDistance; z++)
            {
                var offsetGridPosition = new GridPosition(x,z);
                var unitPositon = unit.GetCurrentGridPosition();
                var testGridPosition = offsetGridPosition + unitPositon;
                if(LevelGrid.Instance.TryGetGridObject(testGridPosition, out GridObject gridObject))
                {
                    if (!gridObject.IsEmpty())
                    {
                        continue;
                    }
                    if (Pathfinding.Instance.FindPath(unit.GetCurrentGridPosition(),testGridPosition,out int length) is null)
                    {
                        continue;
                    }
                    var distanceMultyplied = maxMoveDistance*10;
                    if(length > distanceMultyplied)
                    {
                        continue;
                    }
                    validPositions.Add(testGridPosition);
                }
            }
        }
        return validPositions;
    }
    public override void Execute(Action onActionDone)
    {
        Move(MouseWorld.MousePosition());
        ActionStart(onActionDone);
    }
    public override void Execute(Action onActionDone, GridPosition gridPosition)
    {
        var worldPosition = LevelGrid.Instance.GetWorldPosition(gridPosition);
        Move(worldPosition);
        ActionStart(onActionDone);
    }

    public override bool CanExecute()
    {
        var worldPosition = MouseWorld.MousePosition();
        var newTargetPosition = LevelGrid.Instance.GetGridPosition(worldPosition);
        if (IsValidGridPosition(newTargetPosition))
        {
            return true;
        }
        return false;
    }

    protected override EnemyAIAction GetEnemyAIAction(GridPosition gridPosition)
    {
        ShootAction shootAction=unit.GetAction<ShootAction>();
        return new EnemyAIAction
        {
            gridPosition=gridPosition,
            actionValue= shootAction is not null ? shootAction.GetTargetsAvailableFromPosition(gridPosition)*10 : 0,
        };
    }
}
