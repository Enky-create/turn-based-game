using System;
using System.Collections.Generic;
using UnityEngine;

public class TurnAction : BaseAction
{
    [SerializeField] private float rotationSpeed=100;

    protected override void Awake()
    {
        base.Awake();
        actionName = "Spin";
    }

    private float turnAmount=0;
    void Update()
    {
        if (!isActive)
        {
            return;
        }
        var addAmount = rotationSpeed*Time.deltaTime;
        transform.eulerAngles +=  new Vector3(0,addAmount,0);
        turnAmount+=addAmount;
        if (turnAmount >= 360f)
        {
            isActive=false;
            OnActionDone();
            turnAmount=0;
        }
    }
    public override void Execute(Action onActionDone)
    {
        if (LevelGrid.Instance.TryGetGridPosition(MouseWorld.MousePosition(), out GridPosition gridPosition))
        {
            if (unit.GetCurrentGridPosition() == gridPosition)
            {
                base.Execute(onActionDone);
                isActive=true;
            }
            else
            {
                onActionDone();
                return;
            }
        }
        else
        {
            onActionDone();
                return;
        }
        
        
    }

    public override List<GridPosition> GetValidGridPositionList()
    {
        var list = new List<GridPosition>();
        list.Add(unit.GetCurrentGridPosition());
        return list;
    }
}
