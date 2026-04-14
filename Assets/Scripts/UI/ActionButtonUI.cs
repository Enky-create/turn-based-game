using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System;

public class ActionButtonUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI actionName;
    [SerializeField] private Button button;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void SetName(string name)
    {
        actionName.text=name;
    }
    public void SetAction(BaseAction action)
    {
        button.onClick.AddListener(()=>UnitActionSystem.Instance.SetSelectedAction(action));
    }
}
