using System;
using Unity.VisualScripting;
using UnityEngine;

public class UnitActionSystem : MonoBehaviour
{
    [SerializeField] private Unit selectedUnit;
    [SerializeField] private LayerMask unitLayerMask;
    public static UnitActionSystem Instance { get; private set; }
    public event EventHandler<SelectedUnitEventArgs> OnSelectedUnit;
    public class SelectedUnitEventArgs: EventArgs
    {
        public Unit selectedUnit;
    }
    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("There is more than one UnitActionSystem " 
                + transform + " - " + Instance);
            Destroy(gameObject);
            return;
        }
        Instance=this;
    }
    private void Start()
    {
        OnSelectedUnit?.Invoke(this, new SelectedUnitEventArgs { selectedUnit = this.selectedUnit });
    }
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (TryHandleSelectionOfUnit()) return;
            selectedUnit?.Move(MouseWorld.MousePosition());
        }
    }
    private bool TryHandleSelectionOfUnit()
    {
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if(Physics.Raycast(ray, out RaycastHit hitInfo, float.MaxValue, unitLayerMask))
        {
            if(hitInfo.transform.TryGetComponent<Unit>(out Unit unit))
            {
                SetSelectedUnit(unit);
                return true;
            }
        }
        return false;
    }
    private void SetSelectedUnit(Unit unit)
    {
        selectedUnit = unit;
        OnSelectedUnit?.Invoke(this, new SelectedUnitEventArgs { selectedUnit = unit });
    }
    public Unit GetSelectedUnit()
    {
        return selectedUnit;
    }
}
