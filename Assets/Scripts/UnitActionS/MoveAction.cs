using System.Collections.Generic;
using UnityEngine;

public class MoveAction : MonoBehaviour
{
    private GridPosition targetPosition;
    [SerializeField] private int maxMoveDistance;
    private Unit unit;
    private Animator animator;

    [SerializeField] private int speed = 7;
    [SerializeField] private int rotationSpeed = 10;

    private void Awake()
    {
        unit = GetComponent<Unit>();
        animator = unit.GetAnimator();

    }
    private void Start()
    {
        targetPosition = unit.GetCurrentGridPosition();
    }
    void Update()
    {
        var stoppingDistance = 0.1;
        var worldPosition = LevelGrid.Instance.GetWorldPosition(targetPosition);
        if (Vector3.Distance(transform.position, worldPosition) > stoppingDistance)
        {
            Vector3 moveDirection = (worldPosition - this.transform.position).normalized;
            this.transform.position += moveDirection * speed * Time.deltaTime;
            animator.SetBool("IsWalking", true);
            transform.forward = Vector3.Lerp(transform.forward, moveDirection, Time.deltaTime * rotationSpeed);

        }
        else
        {
            animator.SetBool("IsWalking", false);
        }
    }
    public void Move(Vector3 worldPosition)
    {
        var newTargetPosition = LevelGrid.Instance.GetGridPosition(worldPosition);
        if (IsValidGridPosition(newTargetPosition))
        {
            this.targetPosition = newTargetPosition;
        }
    }

    private bool IsValidGridPosition(GridPosition gridPosition)
    {
        return GetValidGridPositionList().Contains(gridPosition);
    }
    
    public List<GridPosition> GetValidGridPositionList()
    {
        var validPositions = new List<GridPosition>();
        for (int x= -maxMoveDistance; x<=maxMoveDistance; x++)
        {
            for (int z= -maxMoveDistance; z<=maxMoveDistance; z++)
            {
                var offsetGridPosition = new GridPosition(x,z);
                var unitPositon = unit.GetCurrentGridPosition();
                var testGridPosition = offsetGridPosition + unitPositon;
                if(LevelGrid.Instance.TryGetGridObject(testGridPosition, out GridObject gridObject))
                {
                    if (gridObject.IsEmpty())
                    {
                        validPositions.Add(testGridPosition);
                        Debug.Log(testGridPosition);
                    }
                }
            }
        }
        return validPositions;
    }
}
