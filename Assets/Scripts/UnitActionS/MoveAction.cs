using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MoveAction : BaseAction
{
    private GridPosition targetPosition;
    [SerializeField] private int maxMoveDistance;
    

    [SerializeField] private int speed = 7;
    [SerializeField] private int rotationSpeed = 10;
    protected override void Awake()
    {
        base.Awake();
        actionName = "Move";
    }
    private void Start()
    {
        targetPosition = unit.GetCurrentGridPosition();
    }
    void Update()
    {
        if (!isActive)
        {
            return;
        }
        var stoppingDistance = 0.1;
        var worldPosition = LevelGrid.Instance.GetWorldPosition(targetPosition);
        if (Vector3.Distance(transform.position, worldPosition) > stoppingDistance)
        {
            Vector3 moveDirection = (worldPosition - this.transform.position).normalized;
            this.transform.position += moveDirection * speed * Time.deltaTime;
            animator.SetBool("IsWalking", true);
            transform.forward = Vector3.Lerp(transform.forward, moveDirection, Time.deltaTime * rotationSpeed);

        }
        else
        {
            animator.SetBool("IsWalking", false);
            isActive=false;
            OnActionDone();
        }
    }
    public void Move(Vector3 worldPosition)
    {
        var newTargetPosition = LevelGrid.Instance.GetGridPosition(worldPosition);
        if (IsValidGridPosition(newTargetPosition))
        {
            this.targetPosition = newTargetPosition;
            isActive=true;
        }
        else
        {
            OnActionDone();
        }
    }

    private bool IsValidGridPosition(GridPosition gridPosition)
    {
        return GetValidGridPositionList().Contains(gridPosition);
    }
    
    public List<GridPosition> GetValidGridPositionList()
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
                    if (gridObject.IsEmpty())
                    {
                        validPositions.Add(testGridPosition);
                    }
                }
            }
        }
        return validPositions;
    }
    public override void Execute(Action onActionDone)
    {
        base.Execute(onActionDone);
        Move(MouseWorld.MousePosition());
    }
}
