using UnityEngine;

public class CrateDestroyedVisuals : MonoBehaviour
{
    [SerializeField] private float lifeTime=3.0f;

    void Start()
    {
        ApplyForceToParts(transform);
    }
    void Update()
    {
        lifeTime-=Time.deltaTime;
        if (lifeTime < 0)
        {
            Destroy(gameObject);
        }
    }
    private void ApplyForceToParts(Transform cloneBone)
    {
        foreach (Transform child in cloneBone)
        {
            if(child.TryGetComponent<Rigidbody>(out Rigidbody rigidBody))
            {
                var explotionForce = 300f;
                var selectedUnitPosition = transform.position;
                var explotionPosition = transform.position;
                var explotionRange = 10f;
                rigidBody.AddExplosionForce(explotionForce,explotionPosition,explotionRange);
            }
            
        }
    }
}
