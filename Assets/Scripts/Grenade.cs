using System;
using System.Runtime.Serialization;
using Unity.VisualScripting;
using UnityEngine;

public class Grenade : MonoBehaviour
{
    public static EventHandler ON_ANY_GRENADE_EXPLOTION;
    private Action onActionComplete;
    private Vector3 targetPosition;
    private float minimumExplotionDistance=.2f;
    private float distance;
    [SerializeField] private float explotionRadius=7f;
    [SerializeField] private int speed = 15;
    [SerializeField] private int damage = 15;
    [SerializeField] private Transform grenadeVFX;
    [SerializeField] private AnimationCurve animationCurve;
    private Vector3 positionXZ;

    public void Setup(Vector3 targetPosition, Action onActionComplete)
    {
        this.targetPosition = targetPosition;
        this.onActionComplete = onActionComplete;
        
        positionXZ=transform.position;
        positionXZ.y=0;
        distance = Vector3.Distance(positionXZ, targetPosition);

    }

    // Update is called once per frame
    void Update()
    {
        var currentDistance= Vector3.Distance(positionXZ, targetPosition);
        var moveDirection = (targetPosition-positionXZ).normalized;

        positionXZ +=  moveDirection*speed*Time.deltaTime;
        var Yposition = distance/4f * animationCurve.Evaluate(1-currentDistance/distance);
        transform.position =  new Vector3(positionXZ.x,Yposition,positionXZ.z);
        if(Vector3.Distance(positionXZ, targetPosition) < minimumExplotionDistance)
        {
            Collider [] colliders = Physics.OverlapSphere(transform.position,explotionRadius);
            foreach( Collider collider in colliders){
                if (collider.transform.TryGetComponent<Unit>(out Unit unit))
                {
                    unit.Damage(damage);
                }
            }
            onActionComplete();
            Instantiate(grenadeVFX,transform.position,Quaternion.identity);
            ON_ANY_GRENADE_EXPLOTION?.Invoke(this,EventArgs.Empty);
            Destroy(gameObject);
        }
    }
}
