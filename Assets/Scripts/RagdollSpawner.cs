using System;
using UnityEngine;

public class RagdollSpawner : MonoBehaviour
{
    [SerializeField] private Transform ragdolTransform;
    [SerializeField] private Transform originalBone;
    private Unit unit;
    void Awake()
    {
        unit = GetComponent<Unit>();
    }
    void Start()
    {
        unit.OnUnitDying+=unit_OnUnitDying;
    }
    private void unit_OnUnitDying(object sender, EventArgs e)
    {
        var ragdollInstance = Instantiate(ragdolTransform,
         transform.position,
         Quaternion.identity).GetComponent<Ragdoll>();
        ragdollInstance.Setup(originalBone);
    }
    void OnDestroy()
    {
        unit.OnUnitDying-=unit_OnUnitDying;
    }
}
