using System;
using System.Collections.Generic;
using UnityEngine;

public class ActionContainerUI : MonoBehaviour
{
    [SerializeField] ActionButtonUI buttonPrefab;
    private List<ActionButtonUI> buttons;
    private void Awake() {
        buttons = new List<ActionButtonUI>();
    }
    void Start()
    {
        UnitActionSystem.Instance.OnSelectedUnit+=Instance_OnselectedUnit;
        UnitActionSystem.Instance.OnSelectedActionChange+=Instance_OnSelectedActionChange;
        onSelectedUnitChanged();
        OnSelectedActionChange();
    }
    void OnDestroy()
    {
        UnitActionSystem.Instance.OnSelectedActionChange-=Instance_OnSelectedActionChange;
        UnitActionSystem.Instance.OnSelectedUnit-=Instance_OnselectedUnit;
    }
    private void Instance_OnSelectedActionChange(object sender, EventArgs e)
    {
        OnSelectedActionChange();
    }
    private void OnSelectedActionChange()
    {
        var selectedAction = UnitActionSystem.Instance.GetSelectedAction();
        foreach(ActionButtonUI button in buttons)
        {
            var buttonAction = button.GetButtonAction();
            if(buttonAction == selectedAction)
            {
                button.EnableSelectedImage();
            }
            else
            {
                button.DisableSelectedImage();
            }
        }
    }
    
    private void Instance_OnselectedUnit(object sender, UnitActionSystem.SelectedUnitEventArgs e)
    {
        onSelectedUnitChanged();
    }
    private void onSelectedUnitChanged()
    {
        var actions = UnitActionSystem.Instance.GetSelectedUnit().GetBaseActions();
        foreach (Transform child in transform) {
            Destroy(child.gameObject);
            buttons.Clear();
        }
        foreach(BaseAction action in actions)
        {
            ActionButtonUI actionButton = Instantiate(buttonPrefab,transform).GetComponent<ActionButtonUI>();
            actionButton.SetAction(action);
            actionButton.SetName(action.GetName());
            buttons.Add(actionButton);
        }
    }
}
