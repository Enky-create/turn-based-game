using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseAction : MonoBehaviour
{
    protected bool isActive = false;
    protected string actionName = "";
    protected Unit unit;
    protected Animator animator;
    protected Action OnActionDone;
    protected Action OnActionStart;
    protected virtual void Awake() 
    {
        unit = GetComponent<Unit>();
        animator = unit.GetAnimator();
        
    }
    public virtual void Execute(Action onActionDone, Action onActionStart)
    {
        OnActionDone = onActionDone;
        OnActionStart = onActionStart;
        //isActive=true;
    }
    public string GetName()
    {
        return actionName;
    }
    public abstract List<GridPosition> GetValidGridPositionList();
    public virtual int GetActionPointsCost()
    {
        return 1;
    }
}
