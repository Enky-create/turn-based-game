using System;
using NUnit.Framework;
using UnityEngine;

public class Unit : MonoBehaviour
{
    public static event EventHandler OnAnyActionPointChange;   
    [SerializeField] private Animator animator;
    [SerializeField] int maxActionPoints=2;
    [SerializeField] int actionPoints;
    [SerializeField] private bool isEnemy=false;
    private GridPosition currentGridPosition;
    private LevelGrid levelGridInstance;
    private MoveAction moveAction;
    private TurnAction turnAction;
    private BaseAction[] baseActionArray;
    private void Awake()
    {
        
        moveAction = GetComponent<MoveAction>();
        turnAction = GetComponent<TurnAction>();
        baseActionArray = GetComponents<BaseAction>();
        if (LevelGrid.Instance.TryGetGridPosition(transform.position, out GridPosition gridPosition))
        {
            currentGridPosition = gridPosition;
            transform.position = LevelGrid.Instance.GetWorldPosition(currentGridPosition);
            LevelGrid.Instance.TryAddUnit(currentGridPosition,this);
        }
        else
        {
            Debug.LogError("This Unit is not inside GRID " + transform);
        }
        
    }
    void Start()
    {
        TurnSystem.Instance.OnTurnChanged+=TurnSystem_OnTurnChanged;
        
        ResetActionPoints();
    }

    // Update is called once per frame
    void Update()
    {
        var isValid=LevelGrid.Instance.TryGetGridPosition(transform.position, out GridPosition gridPosition);
        if (isValid && currentGridPosition != gridPosition)
        {

            LevelGrid.Instance.UnitMovedPosition(currentGridPosition, gridPosition, this);
            currentGridPosition = gridPosition;
        }
    }
    private void TurnSystem_OnTurnChanged(object sender, EventArgs e)
    {
        ResetActionPoints();
    }
    private void ResetActionPoints()
    {
        if(IsEnemy() && !TurnSystem.Instance.IsPlayerTurn() ||
            !IsEnemy() && TurnSystem.Instance.IsPlayerTurn()
        )
        {
            actionPoints = maxActionPoints;
            OnAnyActionPointChange?.Invoke(this, EventArgs.Empty);
        }
        
    }
    public Animator GetAnimator()
    {
        return animator;
    }
    public MoveAction GetMoveAction() {
        return moveAction;
    }
    public GridPosition GetCurrentGridPosition()
    {
        return currentGridPosition;
    }
    public BaseAction GetTurnAction()
    {
        return turnAction;
    }
    public BaseAction[] GetBaseActions()
    {
        return baseActionArray;
    }
    public void AddActionPoints(int points)
    {
        if (points < 0)
        {
            Debug.LogError("points have negative value");
            return;
        }
        actionPoints += points;
    }
    public void SubstractActionPoints(int points)
    {
        if (points < 0)
        {
            Debug.LogError("points have negative value");
            return;
        }
        actionPoints -= points;
    }
    public bool TrySubstractActionPoints(int points)
    {
        if (actionPoints < points)
        {
            return false;
        }
        SubstractActionPoints(points);
        return true;
    }
    public int GetActionPoints()
    {
        return actionPoints;
    }
    public bool IsEnemy()
    {
        return isEnemy;
    }
}
