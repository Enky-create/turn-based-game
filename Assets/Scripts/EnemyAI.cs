using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class EnemyAI : MonoBehaviour
{
    private enum State
    {
        WaitngForTurn,
        TakingTurn,
        Busy
    }
    private State state;
    private float timer;
    void Awake()
    {
        state = State.WaitngForTurn;
    }
    void Start()
    {
        TurnSystem.Instance.OnTurnChanged+= OnTurnChanged;
    }
    void Update()
    {
        if (TurnSystem.Instance.IsPlayerTurn())
        {
            return;
        }
        switch (state)
        {
            case State.WaitngForTurn:
            break;
            case State.TakingTurn:
            timer-=Time.deltaTime;
            if (timer <= 0)
            {
                //TurnSystem.Instance.NextTurn();
                state = State.Busy;
                if (!TryTakeEnemyAIAction(SetStateTakingTurn))
                {
                    TurnSystem.Instance.NextTurn();
                    state = State.WaitngForTurn;
                }
            }
            break;
            case State.Busy:
            break;
        }
        
    }
    private void SetStateTakingTurn()
    {
        timer=.5f;
        state = State.TakingTurn;
    }
    private bool TryTakeEnemyAIAction(Action onActionDone)
    {
        List<Unit> enemyUnits = UnitManager.Instance.GetEnemyUnits();
        foreach(Unit unit in enemyUnits)
        {
            if (TryTakeEnemyAIAction(unit, onActionDone))
            {
                return true;
            }
        }
        return false;
    }
    private bool TryTakeEnemyAIAction(Unit unit,Action onActionDone)
    {
        BaseAction turnAction = unit.GetTurnAction();
        GridPosition unitPosition = unit.GetCurrentGridPosition();
        if(!turnAction.IsValidGridPosition(unitPosition))  return false;
        if (!unit.TrySubstractActionPoints(turnAction.GetActionPointsCost()))
        {
            return false;
        }
        turnAction?.Execute(onActionDone);
        return true;
    }
    private void OnTurnChanged(object sender,EventArgs e)
    {
        timer=2f;
        state=State.TakingTurn;
    }
}
