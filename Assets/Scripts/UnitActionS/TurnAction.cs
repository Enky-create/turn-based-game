using System;
using UnityEngine;

public class TurnAction : BaseAction
{
    [SerializeField] private float rotationSpeed=100;

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
        
        base.Execute(onActionDone);
        isActive=true;
    }
}
