using UnityEngine;

public class Unit : MonoBehaviour
{
    
    [SerializeField] private Animator animator;
    [SerializeField] int actionPoints=2;
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
}
