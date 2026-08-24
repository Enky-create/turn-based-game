using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private TrailRenderer trailRenderer;
    [SerializeField] private ParticleSystem bulletVFX;
    private bool isActive;
    private Vector3 targetDirection;
    private Vector3 targetPosition;
    private float speed=200f;
    void Awake()
    {
        isActive = false;
    }
    public void Setup(Vector3 targetPosition)
    {
        targetDirection = (targetPosition - transform.position).normalized;
        this.targetPosition=targetPosition;
        isActive=true;
    }
    void Update()
    {
        if (!isActive)
        {
            return;
        }
        var distanceBefore = Vector3.Distance(transform.position,targetPosition);
        transform.position += targetDirection*Time.deltaTime*speed;
        var distanceAfter =Vector3.Distance(transform.position,targetPosition);
        if (distanceAfter > distanceBefore)
        {
            transform.position = targetPosition;
            trailRenderer.transform.parent=null;
            Instantiate(bulletVFX, transform.position,Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
