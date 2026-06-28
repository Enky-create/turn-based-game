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
        var baseActions = unit.GetBaseActions();
        BaseAction bestAction=null;
        EnemyAIAction bestEnemyAIAction=null;
        foreach (BaseAction baseAction in baseActions)
        {
            if (!unit.CanSubstratctActionPoints(baseAction.GetActionPointsCost()))
            {
                continue;
            }
            if (bestAction==null)
            {
                bestAction = baseAction;
                bestEnemyAIAction = baseAction.GetEnemyAIBestAction();
            }
            else
            {
                EnemyAIAction testEnemyAIAction = baseAction.GetEnemyAIBestAction();
                if (testEnemyAIAction!=null && testEnemyAIAction.actionValue>bestEnemyAIAction.actionValue)
                {
                    bestEnemyAIAction=testEnemyAIAction;
                    bestAction = baseAction;
                }
            }
        }
        if(bestAction!=null && unit.TrySubstractActionPoints(bestAction.GetActionPointsCost()))
        {
            bestAction.Execute(onActionDone,bestEnemyAIAction.gridPosition);
            return true;
        }
        else
        {
            return false;
        }
    }
    private void OnTurnChanged(object sender,EventArgs e)
    {
        timer=2f;
        state=State.TakingTurn;
    }
}
