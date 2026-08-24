using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseAction : MonoBehaviour
{
    public static EventHandler OnAnyActionStart;
    public static EventHandler OnAnyActionEnd;
    protected bool isActive = false;
    protected string actionName = "";
    protected Unit unit;
    protected Animator animator;
    protected Action OnActionDone;
    public EventHandler OnActionStart;
    public EventHandler OnActionEnd;
    protected virtual void Awake() 
    {
        unit = GetComponent<Unit>();
        
    }
    
    public virtual void Execute(Action onActionDone, GridPosition gridPosition)
    {
        OnActionDone = onActionDone;
    }
    public virtual void Execute(Action onActionDone)
    {
        OnActionDone = onActionDone;
    }
    public string GetName()
    {
        return actionName;
    }
    public abstract List<GridPosition> GetValidGridPositionList();
    public abstract bool IsValidGridPosition(GridPosition gridPosition);
    public abstract bool CanExecute();
    public virtual int GetActionPointsCost()
    {
        return 1;
    }
    
    protected void ActionStart(Action onActionDone){
        isActive=true;
        OnActionDone=onActionDone;
        OnAnyActionStart?.Invoke(this,EventArgs.Empty);
    }
    protected void ActionEnd(){
        isActive=false;
        OnActionDone?.Invoke();
        OnAnyActionEnd?.Invoke(this,EventArgs.Empty);
    }
    public EnemyAIAction GetEnemyAIBestAction()
    {
        List<GridPosition> validPositions = GetValidGridPositionList();
        List<EnemyAIAction> enemyAIs = new List<EnemyAIAction>();
        foreach(GridPosition position in validPositions)
        {
            enemyAIs.Add(GetEnemyAIAction(position));
        }
        if (enemyAIs.Count == 0)
        {
            return null;
        }
        enemyAIs.Sort((EnemyAIAction a, EnemyAIAction b)=>b.actionValue-a.actionValue);
        return enemyAIs[0];
    }
    protected abstract EnemyAIAction GetEnemyAIAction(GridPosition gridPosition);
}
