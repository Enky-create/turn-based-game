using System;
using UnityEngine;

public class Crate : MonoBehaviour,IDestractable
{
    public static EventHandler ON_ANY_CRATE_DESTROYED;
    public void Damage(int dmg)
    {
        ON_ANY_CRATE_DESTROYED?.Invoke(this,EventArgs.Empty);
        Destroy(gameObject);
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
