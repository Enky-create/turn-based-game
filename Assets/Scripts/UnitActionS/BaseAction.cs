using System;
using UnityEngine;

public abstract class BaseAction : MonoBehaviour
{
    protected bool isActive = false;
    protected Unit unit;
    protected Animator animator;
    protected Action OnActionDone;
    protected virtual void Awake() 
    {
        unit = GetComponent<Unit>();
        animator = unit.GetAnimator();
        
    }
    public virtual void Execute(Action onActionDone)
    {
        OnActionDone = onActionDone;
        //isActive=true;
    }
}
