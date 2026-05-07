using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
public class UnitActionSystem : MonoBehaviour
{
    [SerializeField] private Unit selectedUnit;
    [SerializeField] private LayerMask unitLayerMask;
    private bool isBusy=false;
    private BaseAction selectedAction;
    public static UnitActionSystem Instance { get; private set; }
    public event EventHandler<SelectedUnitEventArgs> OnSelectedUnit;
    public event EventHandler<IsBusyChangedEventArgs> OnIsBusyChanged;
    public event EventHandler OnSelectedActionChange;
    public event EventHandler OnActionStart;
    public class IsBusyChangedEventArgs : EventArgs
    {
        public bool isBusy;
    }
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
        OnSelectedActionChange?.Invoke(this,EventArgs.Empty);
        OnSelectedUnit?.Invoke(this, new SelectedUnitEventArgs { selectedUnit = this.selectedUnit });
    }
    void Update()
    {
        if (isBusy)
        {
            return;
        }
        if (!TurnSystem.Instance.IsPlayerTurn())
        {
            return;
        }
        if (EventSystem.current.IsPointerOverGameObject())
        {
            return;
        }
        if (Input.GetMouseButtonDown(0))
        {
            if (TryHandleSelectionOfUnit()) return;
            var points = selectedAction.GetActionPointsCost();
            if(!selectedAction.CanExecute())  return;
            if (!selectedUnit.TrySubstractActionPoints(points))
            {
                return;
            }
            SetIsBusy();
            selectedAction?.Execute(OnActionIsDone);
            OnActionStart?.Invoke(this,EventArgs.Empty);
            
        }
        if (Input.GetMouseButtonDown(1))
        {
            SetIsBusy();
            selectedUnit?.GetTurnAction().Execute(OnActionIsDone);
        }
    }
    private bool TryHandleSelectionOfUnit()
    {
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if(Physics.Raycast(ray, out RaycastHit hitInfo, float.MaxValue, unitLayerMask))
        {
            if(hitInfo.transform.TryGetComponent<Unit>(out Unit unit))
            {
                if(selectedUnit==unit) return false;
                if(unit.IsEnemy()) return false;
                SetSelectedUnit(unit);
                SetSelectedAction(unit.GetMoveAction());
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
        OnSelectedActionChange?.Invoke(this,EventArgs.Empty);
    }
    public BaseAction GetSelectedAction()
    {
        return selectedAction;
    }
    private void SetIsBusy()
    {
        isBusy=true;
        OnIsBusyChanged?.Invoke(this, new IsBusyChangedEventArgs{isBusy=isBusy});
    }
    private void ClearIsbusy()
    {
        isBusy = false;
        OnIsBusyChanged?.Invoke(this, new IsBusyChangedEventArgs{isBusy=isBusy});
    }
    private void OnActionIsDone()
    {
        ClearIsbusy();
        OnSelectedActionChange?.Invoke(this,EventArgs.Empty);
    }
    public Unit GetSelectedUnit()
    {
        return selectedUnit;
    }
}
