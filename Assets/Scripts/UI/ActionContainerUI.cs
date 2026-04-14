using System;
using UnityEngine;

public class ActionContainerUI : MonoBehaviour
{
    [SerializeField] ActionButtonUI buttonPrefab;
    void Start()
    {
        UnitActionSystem.Instance.OnSelectedUnit+=Instance_OnselectedUnit;
        onSelectedUnitChanged();
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
        }
        foreach(BaseAction action in actions)
        {
            ActionButtonUI actionButton = Instantiate(buttonPrefab,transform).GetComponent<ActionButtonUI>();
            actionButton.SetAction(action);
            actionButton.SetName(action.GetName());
        }
    }
}
