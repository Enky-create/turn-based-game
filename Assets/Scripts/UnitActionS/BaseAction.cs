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
    public EventHandler OnActionStart;
    public EventHandler OnActionEnd;
    protected virtual void Awake() 
    {
        unit = GetComponent<Unit>();
        
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
    public abstract bool CanExecute();
    public virtual int GetActionPointsCost()
    {
        return 1;
    }
    protected void ActionStart(Action onActionDone){
        isActive=true;
        OnActionDone=onActionDone;
    }
    protected void ActionEnd(){
        isActive=false;
        OnActionDone?.Invoke();
    }
}
