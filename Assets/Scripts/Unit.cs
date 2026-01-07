using UnityEngine;

public class Unit : MonoBehaviour
{
    private Vector3 targetPosition;
    [SerializeField] private int speed = 7;
    [SerializeField] private Animator animator;
    [SerializeField] private int rotationSpeed = 10;
    public void Move( Vector3 targetPosition)
    {
        this.targetPosition = targetPosition;
    }

    void Start()
    {
        targetPosition=transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        
        
        var stoppingDistance = 0.1;
        if (Vector3.Distance(transform.position, targetPosition) > stoppingDistance)
        {
            Vector3 moveDirection = (targetPosition - this.transform.position).normalized;
            this.transform.position += moveDirection * speed * Time.deltaTime;
            animator.SetBool("IsWalking", true);
            transform.forward = Vector3.Lerp(transform.forward, moveDirection,Time.deltaTime*rotationSpeed);
        }
        else
        {
            animator.SetBool("IsWalking", false);
        }
    }
}
