using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System;
using Unity.VisualScripting;

public class ActionButtonUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI actionName;
    [SerializeField] private Button button;
    [SerializeField] private Image selectedButtonImage;
    private BaseAction buttonAction;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void SetName(string name)
    {
        actionName.text=name;
    }
    public void SetAction(BaseAction action)
    {
        buttonAction = action;
        button.onClick.AddListener(()=>UnitActionSystem.Instance.SetSelectedAction(action));
    }
    public BaseAction GetButtonAction()
    {
        return buttonAction;
    }
    public void EnableSelectedImage()
    {
        selectedButtonImage.enabled=true;
    }
    public void DisableSelectedImage()
    {
        selectedButtonImage.enabled=false;
    }
}
