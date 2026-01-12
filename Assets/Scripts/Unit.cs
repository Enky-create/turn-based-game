using UnityEngine;

public class Unit : MonoBehaviour
{
    private Vector3 targetPosition;
    [SerializeField] private int speed = 7;
    [SerializeField] private Animator animator;
    [SerializeField] private int rotationSpeed = 10;
    private GridPosition currentGridPosition;
    private LevelGrid levelGridInstance;
    public void Move( Vector3 targetPosition)
    {
        this.targetPosition = targetPosition;
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
        targetPosition=transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        var stoppingDistance = 0.1;
        if (Vector3.Distance(transform.position, targetPosition) > stoppingDistance 
        && LevelGrid.Instance.TryGetGridPosition(targetPosition, out GridPosition gridPosition))
        {
            Vector3 moveDirection = (targetPosition - this.transform.position).normalized;
            this.transform.position += moveDirection * speed * Time.deltaTime;
            animator.SetBool("IsWalking", true);
            transform.forward = Vector3.Lerp(transform.forward, moveDirection,Time.deltaTime*rotationSpeed);

            if (currentGridPosition!=gridPosition)
            {
                
                LevelGrid.Instance.UnitMovedPosition(currentGridPosition,gridPosition,this);
                currentGridPosition = gridPosition;
            }
        }
        else
        {
            animator.SetBool("IsWalking", false);
        }
    }
}
