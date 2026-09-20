using System;
using Unity.VisualScripting;
using UnityEngine;

public class Grenade : MonoBehaviour
{
    private Action onActionComplete;
    private Vector3 targetPosition;
    private float minimumExplotionDistance=.2f;
    [SerializeField] private float explotionRadius=7f;
    [SerializeField] private int speed = 15;
    [SerializeField] private int damage = 15;
    public void Setup(Vector3 targetPosition, Action onActionComplete)
    {
        this.targetPosition = targetPosition;
        this.onActionComplete = onActionComplete;
    }

    // Update is called once per frame
    void Update()
    {
        var moveDirection = (targetPosition - transform.position).normalized;
        transform.position+= moveDirection * speed * Time.deltaTime;
        if(Vector3.Distance(transform.position, targetPosition) < minimumExplotionDistance)
        {
            Collider [] colliders = Physics.OverlapSphere(transform.position,explotionRadius);
            foreach( Collider collider in colliders){
                if (collider.transform.TryGetComponent<Unit>(out Unit unit))
                {
                    unit.Damage(damage);
                }
            }
            onActionComplete();
            Destroy(gameObject);
        }
    }
}
