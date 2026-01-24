using UnityEngine;

public class Unit : MonoBehaviour
{
    
    [SerializeField] private Animator animator;
    
    private GridPosition currentGridPosition;
    private LevelGrid levelGridInstance;
    private MoveAction moveAction;

    private void Awake()
    {
        moveAction = GetComponent<MoveAction>();
    }
    void Start()
    {
        if (LevelGrid.Instance.TryGetGridPosition(transform.position, out GridPosition gridPosition))
        {
            currentGridPosition = gridPosition;
        }
        else
        {
            Debug.LogError("This Unit is not inside GRID " + transform);
        }
        
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
}
