using UnityEngine;

public class SelectedUnitVisual : MonoBehaviour
{
    [SerializeField] private Unit unit;
    private MeshRenderer meshRenderer;
    void Awake()
    {
        meshRenderer=GetComponent<MeshRenderer>();
        meshRenderer.enabled=false;
    }
    private void Start()
    {
        UnitActionSystem.Instance.OnSelectedUnit += Instance_OnSelectedUnit;
        UpdateVisual();
    }

    private void Instance_OnSelectedUnit(object sender, UnitActionSystem.SelectedUnitEventArgs e)
    {
        UpdateVisual();
    }
    private void OnDestroy()
    {
        UnitActionSystem.Instance.OnSelectedUnit -= Instance_OnSelectedUnit;
    }
    private void UpdateVisual()
    {
        if (unit == UnitActionSystem.Instance.GetSelectedUnit())
        {
            meshRenderer.enabled = true;
        }
        else
        {
            meshRenderer.enabled = false;
        }
    }
}
