using System;
using Unity.VisualScripting;
using UnityEngine;

public class UnitActionSystem : MonoBehaviour
{
    [SerializeField] private Unit selectedUnit;
    [SerializeField] private LayerMask unitLayerMask;
    private bool isBusy=false;
    private BaseAction selectedAction;
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
        selectedAction=selectedUnit.GetMoveAction();
        OnSelectedUnit?.Invoke(this, new SelectedUnitEventArgs { selectedUnit = this.selectedUnit });
    }
    void Update()
    {
        if (isBusy)
        {
            return;
        }
        if (Input.GetMouseButtonDown(0))
        {
            if (TryHandleSelectionOfUnit()) return;
            SetIsBusy();
            selectedAction?.Execute(ClearIsbusy);
            
        }
        if (Input.GetMouseButtonDown(1))
        {
            SetIsBusy();
            selectedUnit?.GetTurnAction().Execute(ClearIsbusy);
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
                selectedAction=unit.GetMoveAction();
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
    public void SetSelectedAction(BaseAction baseAction)
    {
        selectedAction = baseAction;
    }
    private void SetIsBusy()
    {
        isBusy=true;
    }
    private void ClearIsbusy()
    {
        isBusy = false;
    }
    public Unit GetSelectedUnit()
    {
        return selectedUnit;
    }
}
