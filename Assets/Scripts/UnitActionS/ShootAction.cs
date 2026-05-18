using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.Animations;

public class ShootAction : BaseAction
{
    [SerializeField] private int maxShootDistance;
    [SerializeField] private float damage = 3f;
    private Unit targetUnit;
    [SerializeField] private int aimingSpeed = 10;
    private float timer;
    private enum ShootStateEnum
    {
        Aiming,
        Shooting,
        Idling,
    }
    private ShootStateEnum currentState = ShootStateEnum.Idling;
    private Vector3 aimDirection;
    void Start()
    {
        actionName = "Shoot";
    }

    // Update is called once per frame
    void Update()
    {
        if (!isActive)
        {
            return;
        }
        timer-=Time.deltaTime;
        switch (currentState)
        {
            case ShootStateEnum.Aiming:
                unit.transform.forward = Vector3.Lerp(
                    transform.forward,
                    aimDirection,
                    aimingSpeed*Time.deltaTime
                    );
                
                
            break;
        }
        if (timer <= 0)
        {
            NextState();
        }
    }

    private void NextState()
    {
        switch (currentState)
        {
            case ShootStateEnum.Idling:
                timer = .5f;
                currentState=ShootStateEnum.Aiming;
            break;
            case ShootStateEnum.Aiming:
                timer = 2f;
                currentState=ShootStateEnum.Shooting;
            break;
            case ShootStateEnum.Shooting:
                timer = .5f;
                targetUnit.Damage(damage);
                currentState=ShootStateEnum.Idling;
                OnActionDone?.Invoke();
                isActive = false;
            break;
        }
    }

    public override bool CanExecute()
    {
        var worldPosition = MouseWorld.MousePosition();
        var newTargetPosition = LevelGrid.Instance.GetGridPosition(worldPosition);
        if (GetValidGridPositionList().Contains(newTargetPosition))
        {
            return true;
        }
        return false;
    }
    public override void Execute(Action onActionDone)
    {
        OnActionDone=onActionDone;
        if (LevelGrid.Instance.TryGetGridObject(MouseWorld.MousePosition(), out GridObject gridObject))
        {
            this.targetUnit = gridObject.GetFirstUnitInList();
            aimDirection=(targetUnit.transform.position - unit.transform.position).normalized;
            
        }
        
        isActive=true;
    }
    public override List<GridPosition> GetValidGridPositionList()
    {
        var validPositions = new List<GridPosition>();
        for (int x= -maxShootDistance; x<=maxShootDistance; x++)
        {
            for (int z= -maxShootDistance; z<=maxShootDistance; z++)
            {
                var offsetGridPosition = new GridPosition(x,z);
                var unitPositon = unit.GetCurrentGridPosition();
                var testGridPosition = offsetGridPosition + unitPositon;
                var worldTestPosition = LevelGrid.Instance.GetWorldPosition(testGridPosition);
                if(Vector3.Distance(unit.transform.position, worldTestPosition) > maxShootDistance 
                * LevelGrid.Instance.GetCellSize())
                {
                    continue;
                }
                if(LevelGrid.Instance.TryGetGridObject(testGridPosition, out GridObject gridObject))
                {
                    if (gridObject.IsEmpty())
                    {
                        continue;
                    }
                    var testUnit=gridObject.GetFirstUnitInList();
                    if(testUnit!=unit && testUnit.IsEnemy() != unit.IsEnemy())
                    {
                        validPositions.Add(testGridPosition);
                    }
                }
            }
        }
        return validPositions;
    }
    public override int GetActionPointsCost()
    {
        return 2;
    }
    
}
