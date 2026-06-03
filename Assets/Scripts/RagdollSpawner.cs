using System;
using UnityEngine;

public class RagdollSpawner : MonoBehaviour
{
    [SerializeField] private Transform ragdolTransform;
    [SerializeField] private Transform originalBone;
    private HealthComponent healthComponent;
    void Awake()
    {
        healthComponent = GetComponent<HealthComponent>();
    }
    void Start()
    {
        healthComponent.OnDeath += HealthComponent_OnDeath;
    }
    private void HealthComponent_OnDeath(object sender, EventArgs e)
    {
        var ragdollInstance = Instantiate(ragdolTransform,
         transform.position,
         Quaternion.identity).GetComponent<Ragdoll>();
        ragdollInstance.Setup(originalBone);
    }
    void OnDestroy()
    {
        healthComponent.OnDeath -= HealthComponent_OnDeath;
    }
}
