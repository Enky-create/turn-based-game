using System;
using System.Collections.Generic;
using UnityEngine;

public class GranadeAction : BaseAction
{
    public override bool CanExecute()
    {
        throw new System.NotImplementedException();
    }

    public override List<GridPosition> GetValidGridPositionList()
    {
        throw new System.NotImplementedException();
    }

    public override bool IsValidGridPosition(GridPosition gridPosition)
    {
        throw new System.NotImplementedException();
    }

    protected override EnemyAIAction GetEnemyAIAction(GridPosition gridPosition)
    {
        throw new System.NotImplementedException();
    }
    public override void Execute(Action onActionDone, GridPosition gridPosition)
    {
        base.Execute( onActionDone,  gridPosition);
    }
    public override void Execute(Action onActionDone)
    {
        base.Execute(onActionDone);
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
