using UnityEngine;

public class MoveAction : MonoBehaviour
{
    private Vector3 targetPosition;
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
        targetPosition = unit.transform.position;
    }
    void Update()
    {
        var stoppingDistance = 0.1;
        if (Vector3.Distance(transform.position, targetPosition) > stoppingDistance
        && LevelGrid.Instance.TryGetGridPosition(targetPosition, out GridPosition gridPosition))
        {
            Vector3 moveDirection = (targetPosition - this.transform.position).normalized;
            this.transform.position += moveDirection * speed * Time.deltaTime;
            animator.SetBool("IsWalking", true);
            transform.forward = Vector3.Lerp(transform.forward, moveDirection, Time.deltaTime * rotationSpeed);

        }
        else
        {
            animator.SetBool("IsWalking", false);
        }
    }
    public void Move(Vector3 targetPosition)
    {
        this.targetPosition = targetPosition;
    }
}
