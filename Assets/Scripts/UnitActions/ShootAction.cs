using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.Animations;

public class ShootAction : BaseAction
{
    [SerializeField] private int maxShootDistance;
    [SerializeField] private int damage = 3;
    [SerializeField] private int aimingSpeed = 10;
    public class ShootEventArgs: EventArgs{
        public Transform target;
    }
    private Unit targetUnit;
    private float timer;
    private ShootStateEnum currentState;
    private Vector3 aimDirection;
    private bool canShoot=false;
    private enum ShootStateEnum
    {
        Aiming,
        Shooting,
        CoolOff,
    }
    
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
        
        switch (currentState)
        {
            
            case ShootStateEnum.Aiming:
                unit.transform.forward = Vector3.Lerp(
                    transform.forward,
                    aimDirection,
                    aimingSpeed*Time.deltaTime
                    );
                    break;
            case ShootStateEnum.Shooting:
                if(canShoot){
                    
                    OnActionStart?.Invoke(this, new ShootEventArgs
                    {
                        target = targetUnit.transform,
                    });
                    canShoot = false;
                }
            break;
            case ShootStateEnum.CoolOff:
                
            break;
                
            
        }
        timer-=Time.deltaTime;
        if (timer <= 0)
        {
            NextState();
        }
    }

    private void NextState()
    {
        switch (currentState)
        {
            case ShootStateEnum.Aiming:
                timer = 0.3f;
                currentState=ShootStateEnum.Shooting;
            break;
            case ShootStateEnum.Shooting:
                timer = .1f;
                currentState = ShootStateEnum.CoolOff;
            break;
            case ShootStateEnum.CoolOff:
                targetUnit.Damage(damage);
                ActionEnd();
            break;
        }
        Debug.Log($"Current state is {currentState}");
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
        
        if (LevelGrid.Instance.TryGetGridObject(MouseWorld.MousePosition(), out GridObject gridObject))
        {
            this.targetUnit = gridObject.GetFirstUnitInList();
            aimDirection=(targetUnit.transform.position - unit.transform.position).normalized;
            
        }
        canShoot = true;
        currentState = ShootStateEnum.Aiming;
        timer = 0.5f;
        ActionStart(onActionDone);
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
    public Unit GetUnit()
    {
        return unit;
    }
    public Unit GetTargetUnit()
    {
        return targetUnit;
    }
    public int GetMaxShootDistance()
    {
        return maxShootDistance;
    }
}
