using System;
using Unity.VisualScripting;
using UnityEngine;

public class Crate : MonoBehaviour,IDestractable
{
    public static EventHandler ON_ANY_CRATE_DESTROYED;
    [SerializeField] private CrateDestroyedVisuals cratePartsVisuals;
    public void Damage(int dmg)
    {
        ON_ANY_CRATE_DESTROYED?.Invoke(this,EventArgs.Empty);
        Instantiate(cratePartsVisuals,transform.position,Quaternion.identity);
        Destroy(gameObject);
    }
}
