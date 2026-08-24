using UnityEngine;
using UnityEngine.UIElements;

public class Ragdoll : MonoBehaviour
{
    [SerializeField] private Transform cloneBone;
    public void Setup(Transform originalBone)
    {
        MatchBones(originalBone,cloneBone);
        ApplyForceToBones(cloneBone);
    }
    private void MatchBones(Transform originalBone, Transform cloneBone)
    {
        foreach (Transform child in originalBone)
        {
            var clone = cloneBone.Find(child.name);
            if (clone != null)
            {
                clone.position = child.position;
                clone.rotation = child.rotation;
                MatchBones(child, clone);
            }
        }
    }
    private void ApplyForceToBones(Transform cloneBone)
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
            
            ApplyForceToBones(child);
        }
    }
}
